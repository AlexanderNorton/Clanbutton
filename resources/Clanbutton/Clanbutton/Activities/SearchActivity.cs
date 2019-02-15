using System;
using System.Collections.Generic;
using System.Collections;
using System.Timers;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;

using Firebase.Auth;

using Clanbutton.Builders;
using Clanbutton.Core;

namespace Clanbutton.Activities
{
    [Activity(Label = "Clanbutton", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class SearchActivity : AppCompatActivity
    {
        private DatabaseHandler firebase_database;
        private FirebaseAuth auth;
        private FirebaseUser user;
        private SteamClient steam_client;

        // Layout
        ImageButton MainButton;
		private Button ProfileButton;
        private Button ChatroomButton;
        private Button CurrentGameButton;
        private Button SpecificGameButton;
        private AutoCompleteTextView SearchContent;
        private List<string> GameList = new List<string>();
        private List<GameSearch> CurrentSearchers = new List<GameSearch>();
        private ArrayList UserList = new ArrayList();
        private ListView PlayerList;

        private string current_game;
        private UserAccount uaccount;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Start the Searching layout.
            SetContentView(Resource.Layout.Searching_Layout);

            // Get references to layout items.
            MainButton = FindViewById<ImageButton>(Resource.Id.mainbutton);
            SearchContent = FindViewById<AutoCompleteTextView>(Resource.Id.searchbar);
            CurrentGameButton = FindViewById<Button>(Resource.Id.current_game_button);
            SpecificGameButton = FindViewById<Button>(Resource.Id.specific_game_button);
			ProfileButton = FindViewById<Button>(Resource.Id.profile_button);


			steam_client = new SteamClient();
            auth = FirebaseAuth.Instance;
            user = auth.CurrentUser;

            firebase_database = new DatabaseHandler();
            uaccount = await firebase_database.GetAccountAsync(user.Uid);

            CurrentGameButton.Click += delegate
            {
                // Start the search for the current game.
                StartSearching(uaccount.PlayingGameName);
            };

            SpecificGameButton.Click += async delegate
            {
                // Get list of all games in the database.
                var items = await steam_client.GetAllSteamGames();

                foreach (var game in items)
                {
                    GameList.Add(game.Name);
                }

                // Fill the auto completer with the list of games.
                ArrayAdapter autoCompleteAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, GameList);
                SearchContent.Adapter = autoCompleteAdapter;

                // Stop showing the specific game button and show the search bar.
                SearchContent.Visibility = Android.Views.ViewStates.Visible;
                SpecificGameButton.Visibility = Android.Views.ViewStates.Gone;
            };

            MainButton.Click += delegate
            {
                StartSearching();
            };

            if (uaccount.PlayingGameName != null && uaccount.PlayingGameName != "")
            {
                // Check if player is currently playing a game and add it as a search option.
                CurrentGameButton.Visibility = Android.Views.ViewStates.Visible;
                CurrentGameButton.Text = $"Search for '{uaccount.PlayingGameName}'";
            }
			ProfileButton.Click += delegate
			{
				ExtensionMethods.OpenUserProfile(uaccount, this);
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

            SetContentView(Resource.Layout.SearchProcess_Layout);
            PlayerList = FindViewById<ListView>(Resource.Id.playerslist);
            ChatroomButton = FindViewById<Button>(Resource.Id.chatroom_button);

            ChatroomButton.Click += delegate
            {
                StartActivity(new Android.Content.Intent(this, typeof(MessagingActivity)));
            };

            // Get all the gamesearches that = the game the user is searching.
            firebase_database = new DatabaseHandler();
            GameSearch game = new GameSearch(search_game, uaccount.UserId.ToString(), uaccount.Username);
            firebase_database.PostGameSearchAsync(game);
            var gamesearches = await firebase_database.GetGameSearchesAsync();

            foreach (var u in gamesearches)
            {
                if (u.Object.GameName == search_game && u.Object.Username.Length > 1)
                {
                    UserList.Add(u.Object.Username);
                    CurrentSearchers.Add(u.Object);
                }
            }

            ArrayAdapter AddPlayerAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleExpandableListItem1, UserList);

            PlayerList.Adapter = AddPlayerAdapter;
            PlayerList.ItemClick += PlayerList_ItemClick;
            PlayerList.PerformClick();
        }

        private async void PlayerList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                GameSearch SelectedSearch = CurrentSearchers[e.Position];

                UserAccount account = await firebase_database.GetAccountAsync(user.Uid);

                ExtensionMethods.OpenUserProfile(account, this);
            }
            catch(Exception exception)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {exception.Message}");
            }

        }
    }
}