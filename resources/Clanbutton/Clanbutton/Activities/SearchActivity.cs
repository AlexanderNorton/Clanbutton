using System;
using System.Collections.Generic;
using System.Collections;
using System.Timers;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;

using Firebase.Auth;
using Firebase.Database;

using Clanbutton.Builders;
using Clanbutton.Core;
using System.Net;

namespace Clanbutton.Activities
{
    [Activity(Label = "Clanbutton", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class SearchActivity : AppCompatActivity, IValueEventListener
    {
        private DatabaseHandler firebase_database;
        private FirebaseAuth auth;
        private FirebaseUser user;
        private SteamClient steam_client;

        // Layout
        private ImageButton MainButton;
        private Button BeaconButton;
        private Button ChatroomButton;
        private Button CurrentGameButton;
        private AutoCompleteTextView SearchContent;
        private List<GameSearch> UserList = new List<GameSearch>();
        private List<string> GameList = new List<string>();
        private List<GameSearch> CurrentSearchers = new List<GameSearch>();
        private ListView PlayerList;

        private UserAccount uaccount;
        public DatabaseReference gamesearches_reference;
        private GamesearchListAdapter adapter;
        private GameSearch game;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Start the Searching layout.
            SetContentView(Resource.Layout.Searching_Layout);

            // Get references to layout items.
            MainButton = FindViewById<ImageButton>(Resource.Id.mainbutton);
            SearchContent = FindViewById<AutoCompleteTextView>(Resource.Id.searchbar);
            CurrentGameButton = FindViewById<Button>(Resource.Id.current_game_button);
            PlayerList = FindViewById<ListView>(Resource.Id.playerslist);
            ChatroomButton = FindViewById<Button>(Resource.Id.chatroom_button);
            BeaconButton = FindViewById<Button>(Resource.Id.beacon_button);

            steam_client = new SteamClient();
            auth = FirebaseAuth.Instance;
            user = auth.CurrentUser;

            firebase_database = new DatabaseHandler();
            uaccount = await firebase_database.GetAccountAsync(user.Uid);

            var items = await steam_client.GetAllSteamGames();

            foreach (var game in items)
            {
                GameList.Add(game.Name);
            }

            // Fill the auto completer with the list of games.
            ArrayAdapter autoCompleteAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, GameList);
            SearchContent.Adapter = autoCompleteAdapter;

            if (uaccount.PlayingGameName != null && uaccount.PlayingGameName != "")
            {
                // Check if player is currently playing a game and add it as a search option.
                CurrentGameButton.Visibility = Android.Views.ViewStates.Visible;
                CurrentGameButton.Text = $"Search for '{uaccount.PlayingGameName}'";
            }

            MainButton.Click += delegate
            {
                StartSearching();
            };

            CurrentGameButton.Click += delegate
            {
                // Start the search for the current game.
                StartSearching(uaccount.PlayingGameName);
            };

            BeaconButton.Click += async delegate
            {
                var current_beacon = await firebase_database.GetBeaconForUser(uaccount.UserId);

                if (current_beacon != null && current_beacon.Object.CreationTime.AddMinutes(30) > DateTime.Now)
                {
                    Toast.MakeText(this, $"You just deployed a beacon for '{current_beacon.Object.GameName}'. Please wait a while before deploying another.", ToastLength.Long).Show();
                    return;
                }
                // Create the beacon.
                new Beacon(uaccount.UserId, SearchContent.Text, DateTime.Now).Create();
                // Create the beacon activity.
                new UserActivity(uaccount.UserId, uaccount.Username, $"Deployed a beacon and wants to play '{SearchContent.Text}'", uaccount.Avatar).Create();
                // Response
                Toast.MakeText(this, $"You have deployed a beacon for '{current_beacon.Object.GameName}'. Your followers have been notified.", ToastLength.Long).Show();
                BeaconButton.Visibility = Android.Views.ViewStates.Gone;
                // TODO: Send a beacon notification to all followers.
            };
        }

        public async void StartSearching(string current_game = null)
        {
            string search_game;
            if (current_game != null)
            {
                search_game = current_game;
            }
            else
            {
                search_game = SearchContent.Text;
            }

            if (SearchContent.Text.Length == 0 && current_game == null)
            {
                Toast.MakeText(this, $"Enter a game title before searching.", ToastLength.Short).Show();
                return;
            }

            SearchContent.Visibility = Android.Views.ViewStates.Gone;
            CurrentGameButton.Visibility = Android.Views.ViewStates.Gone;

            BeaconButton.Visibility = Android.Views.ViewStates.Visible;
            ChatroomButton.Visibility = Android.Views.ViewStates.Visible;
            PlayerList.Visibility = Android.Views.ViewStates.Visible;

            game = new GameSearch(search_game, uaccount.UserId.ToString(), uaccount.Username, uaccount.Avatar);

            var gamesearches = await firebase_database.GetGameSearchesAsync();

            foreach (var u in gamesearches)
            {
                if (u.Object.UserId == uaccount.UserId)
                {
                    firebase_database.RemoveGameSearchAsync(u.Key);
                }
            }

            firebase_database.PostGameSearchAsync(game);

            // Create the game search activity.
            new UserActivity(uaccount.UserId, uaccount.Username, $"Started searching for '{search_game}'", uaccount.Avatar).Create();

            gamesearches_reference = FirebaseDatabase.Instance.GetReference("gamesearches");
            gamesearches_reference.AddValueEventListener(this);

            ChatroomButton.Click += delegate
            {
                MessagingActivity.account = uaccount;
                StartActivity(new Android.Content.Intent(this, typeof(MessagingActivity)));
                gamesearches_reference.RemoveEventListener(this);
            };
        }

        public async void RefreshPlayers()
        {
            UserList.Clear();

            var gamesearches = await firebase_database.GetGameSearchesAsync();

            foreach (var u in gamesearches)
            {
                if (u.Object.GameName == game.GameName)
                {
                    UserList.Add(u.Object);
                }
            }

            adapter = new GamesearchListAdapter(this, UserList);
            PlayerList.Adapter = adapter;
        }

        public void OnCancelled(DatabaseError error)
        {
            throw new NotImplementedException();
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            RefreshPlayers();
        }
    }
}