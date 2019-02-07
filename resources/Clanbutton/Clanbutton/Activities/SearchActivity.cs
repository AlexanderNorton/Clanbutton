using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;

using Firebase.Xamarin.Database;
using Firebase.Auth;

using Clanbutton.Builders;
using Clanbutton.Core;
using System;

namespace Clanbutton.Activities
{
    [Activity(Label = "Clanbutton", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class SearchActivity : AppCompatActivity
    {
        private FirebaseClient firebase;
        FirebaseAuth auth;
        int MyResultCode = 1;

        private Button MainButton;
        private Button ChatroomButton;
        private Button LogoffButton;
        private AutoCompleteTextView SearchContent;
        private List<string> GameList = new List<string>();
        private List<GameSearch> CurrentSearchers = new List<GameSearch>();
        private ArrayList UserList = new ArrayList();
        private ListView PlayerList;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Searching_Layout);
            // ExtensionMethods.UpdateAccountData();

            firebase = new FirebaseClient(GetString(Resource.String.firebase_database_url));
            MainButton = FindViewById<Button>(Resource.Id.mainbutton);
            SearchContent = FindViewById<AutoCompleteTextView>(Resource.Id.searchbar);
            LogoffButton = FindViewById<Button>(Resource.Id.logoff_button);

            var items = await firebase.Child("games").OnceAsync<string>();

            foreach (var game in items)
            {
                GameList.Add(game.Object.ToString());
            }

            ArrayAdapter autoCompleteAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, GameList);
            SearchContent.Adapter = autoCompleteAdapter;

            MainButton.Click += delegate
            {
                StartSearching();
            };

            LogoffButton.Click += delegate
            {
                auth = FirebaseAuth.Instance;
                auth.SignOut();
                StartActivity(new Android.Content.Intent(this, typeof(AuthenticationActivity)));
            };

        }

        public async void StartSearching()
        {

            if (SearchContent.Text.Length == 0)
            {
                Toast.MakeText(this, $"Enter a game title before searching.", ToastLength.Short).Show();
                return;
            }

            if (SearchContent.Text.Length < 2)
            {
                Toast.MakeText(this, $"The title you entered is too short.", ToastLength.Short).Show();
                return;
            }

            SetContentView(Resource.Layout.SearchProcess_Layout);
            PlayerList = FindViewById<ListView>(Resource.Id.playerslist);
            ChatroomButton = FindViewById<Button>(Resource.Id.chatroom_button);

            ChatroomButton.Click += delegate
            {
                StartActivity(new Android.Content.Intent(this, typeof(MessagingActivity)));
            };

            FirebaseUser user = FirebaseAuth.Instance.CurrentUser;

            UserAccount Account = await ExtensionMethods.GetAccountAsync(user.Uid.ToString(), firebase);

            await firebase.Child("gamesearches").PostAsync(new GameSearch(SearchContent.Text, Account.UserId.ToString(), Account.Username));
            var gamesearch = await firebase.Child("gamesearches").OnceAsync<GameSearch>();

            foreach (var u in gamesearch)
            {
                if (u.Object.GameName == SearchContent.Text && u.Object.Username.Length > 1)
                {
                    UserList.Add(u.Object.Username);
                    CurrentSearchers.Add(u.Object);
                }
            }

            ArrayAdapter AddPlayerAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleExpandableListItem1, UserList);

            PlayerList.Adapter = AddPlayerAdapter;
            PlayerList.ItemClick += PlayerList_ItemClick;
            PlayerList.PerformClick();
            //Start the search..
        }

        private async void PlayerList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                GameSearch SelectedSearch = CurrentSearchers[e.Position];

                UserAccount Account = await ExtensionMethods.GetAccountAsync(SelectedSearch.UserId.ToString(), firebase);

                ExtensionMethods.OpenUserProfile(Account, this);
            }
            catch(Exception exception)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {exception.Message}");
            }
        }
    }
}