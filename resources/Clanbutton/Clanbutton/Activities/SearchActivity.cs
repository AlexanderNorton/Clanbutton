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
using System.Net;

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
        private ImageButton MainButton;
        private ImageButton ProfileButton;
        private Button ChatroomButton;
        private Button CurrentGameButton;
        private Button SpecificGameButton;
        private AutoCompleteTextView SearchContent;
        private List<string> GameList = new List<string>();
        private List<GameSearch> CurrentSearchers = new List<GameSearch>();
        private ArrayList UserList = new ArrayList();
        private ListView PlayerList;

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
            ProfileButton = FindViewById<ImageButton>(Resource.Id.profile_button);
            PlayerList = FindViewById<ListView>(Resource.Id.playerslist);
            ChatroomButton = FindViewById<Button>(Resource.Id.chatroom_button);

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
            // Download profile picture.
            WebClient web = new WebClient();
            web.DownloadDataCompleted += new DownloadDataCompletedEventHandler(web_DownloadDataCompleted);
            web.DownloadDataAsync(new Uri(uaccount.Avatar));

            void web_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
            {
                // Set profile picture.
                if (e.Error != null)
                {
                    RunOnUiThread(() =>
                                  Toast.MakeText(this, e.Error.Message, ToastLength.Short).Show());
                }
                else
                {

                    Android.Graphics.Bitmap bm = Android.Graphics.BitmapFactory.DecodeByteArray(e.Result, 0, e.Result.Length);

                    RunOnUiThread(() =>
                    {
                        ProfileButton.SetImageBitmap(bm);
                    });
                }
            }
            // Open User Profile
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


            ChatroomButton.Visibility = Android.Views.ViewStates.Visible;
            PlayerList.Visibility = Android.Views.ViewStates.Visible;
            SearchContent.Visibility = Android.Views.ViewStates.Gone;
            CurrentGameButton.Visibility = Android.Views.ViewStates.Gone;
            SpecificGameButton.Visibility = Android.Views.ViewStates.Gone;
            ChatroomButton.Click += delegate
            {
                StartActivity(new Android.Content.Intent(this, typeof(MessagingActivity)));
            };

            firebase_database = new DatabaseHandler();
            var gamesearches = await firebase_database.GetGameSearchesAsync();

            // Get all the gamesearches that = the game the user is searching.
            GameSearch game = new GameSearch(search_game, uaccount.UserId.ToString(), uaccount.Username);

            foreach (var u in gamesearches)
            {
                if (u.Object.UserId == uaccount.UserId)
                {
                    firebase_database.RemoveGameSearchAsync(u.Key);
                }
                if (u.Object.GameName == search_game)
                {
                    UserList.Add(u.Object.Username);
                    CurrentSearchers.Add(u.Object);
                }
            }

            firebase_database.PostGameSearchAsync(game);

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

                UserAccount account = await firebase_database.GetAccountAsync(SelectedSearch.UserId);

                ExtensionMethods.OpenUserProfile(account, this);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {exception.Message}");
            }

        }
    }
}