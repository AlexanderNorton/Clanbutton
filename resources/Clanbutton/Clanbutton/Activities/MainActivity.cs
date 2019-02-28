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
    public class MainActivity : AppCompatActivity, IValueEventListener
    {
        private DatabaseHandler firebase_database;
        private FirebaseAuth auth;
        private FirebaseUser user;
        private SteamClient steam_client;

        // Layout
        private ImageView ProfileButton;
        private ImageView ClanbuttonLogo;
        private Button StartMatchmakingButton;
        private List<UserActivity> lstActivities = new List<UserActivity>();
        private ListView lstActivityView;
        private TextView Username;
        private LinearLayout MainLayout;

        private ActivityListAdapter adapter;
        private UserAccount uaccount;
        public DatabaseReference activities_reference;


        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Start the Searching layout.
            SetContentView(Resource.Layout.Home_Layout);

            // Get references to layout items.
            ClanbuttonLogo = FindViewById<ImageView>(Resource.Id.mainbutton);
            ProfileButton = FindViewById<ImageView>(Resource.Id.profile_button);
            StartMatchmakingButton = FindViewById<Button>(Resource.Id.start_matchmaking_button);
            lstActivityView = FindViewById<ListView>(Resource.Id.list_of_activities);
            Username = FindViewById<TextView>(Resource.Id.profile_name);
            MainLayout = FindViewById<LinearLayout>(Resource.Id.main_layout);

            ExtensionMethods.StartCacheManager();

            steam_client = new SteamClient();
            auth = FirebaseAuth.Instance;
            user = auth.CurrentUser;

            firebase_database = new DatabaseHandler();
            uaccount = await firebase_database.GetAccountAsync(user.Uid);

            ExtensionMethods extensionMethods = new ExtensionMethods();
            extensionMethods.DownloadPicture(uaccount.Avatar, ProfileButton);

            Username.Text = uaccount.Username;
            // Add a listener to the 'activities' database reference.
            activities_reference = FirebaseDatabase.Instance.GetReference("activities");
            activities_reference.AddValueEventListener(this);

            ClanbuttonLogo.Visibility = Android.Views.ViewStates.Visible;
            StartMatchmakingButton.Visibility = Android.Views.ViewStates.Visible;
            MainLayout.Visibility = Android.Views.ViewStates.Visible;


            // Set layout information.
            ProfileButton.Click += delegate
            {
                // Open the user's profile when the profile picture is clicked.
                activities_reference.RemoveEventListener(this);
                ExtensionMethods.OpenUserProfile(uaccount, this);
            };

            StartMatchmakingButton.Click += delegate
            {
                // Start the gamesearch activity.
                activities_reference.RemoveEventListener(this);
                StartActivity(new Android.Content.Intent(this, typeof(SearchActivity)).SetFlags(Android.Content.ActivityFlags.NoAnimation));
            };
        }

        private async void RefreshActivities()
        {
            // Refresh all of the latest user activities into a new adapter.
            lstActivities.Clear();

            var activities = await firebase_database.GetAllActivities();
            var activitieslst = new List<Firebase.Xamarin.Database.FirebaseObject<UserActivity>>();
            activitieslst.AddRange(activities);
            activitieslst.Reverse();
            var count = 0;
            foreach (var item in activitieslst)
            {
                // Check if the activity's user ID is in the current user's account followers.
                if ((uaccount.Following.Contains(item.Object.UserId) || uaccount.UserId == item.Object.UserId) && count <= 5)
                {
                    UserActivity useractivity = item.Object;
                    lstActivities.Add(item.Object);
                    count += 1;
                }
            }
            adapter = new ActivityListAdapter(this, lstActivities);
            lstActivityView.Adapter = adapter;
        }

        public void OnCancelled(DatabaseError error)
        {
            throw new NotImplementedException();
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            RefreshActivities();
        }
    }
}