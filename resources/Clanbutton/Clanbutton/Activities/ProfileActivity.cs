using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;
using Android.Webkit;

using Firebase.Xamarin.Database;
using Firebase.Auth;

using Clanbutton.Builders;
using Clanbutton.Core;
using System;

namespace Clanbutton.Activities
{
    [Activity(Label = "Clanbutton", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class ProfileActivity : AppCompatActivity
    {
        private FirebaseClient firebase;

        private static UserAccount account;
        private static TextView Profile_Username;
        private static TextView Profile_About;
        private static Button Profile_EditButton;
        private static Button Profile_LogoutButton;
        private static Button Profile_VisitSteamProfile;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserProfile_Layout);

            Profile_Username = FindViewById<TextView>(Resource.Id.profile_username);
            Profile_About = FindViewById<TextView>(Resource.Id.profile_about);
            Profile_EditButton = FindViewById<Button>(Resource.Id.profile_edit_button);
            Profile_LogoutButton = FindViewById<Button>(Resource.Id.profile_logout_button);
            Profile_VisitSteamProfile = FindViewById<Button>(Resource.Id.profile_visit_steam_button);

            //SET PROFILE DATA
            Profile_Username.Text = account.Username;
            Profile_About.Text = account.About;

            if (account.UserId != FirebaseAuth.Instance.CurrentUser.Uid)
            {
                Profile_EditButton.Visibility = Android.Views.ViewStates.Gone;
                Profile_LogoutButton.Visibility = Android.Views.ViewStates.Gone;
            }

            firebase = new FirebaseClient(GetString(Resource.String.firebase_database_url));

            Profile_EditButton.Click += delegate
            {
                StartActivity(new Android.Content.Intent(this, typeof(EditProfileActivity)));
            };

            Profile_LogoutButton.Click += delegate
            {
                FirebaseAuth.Instance.SignOut();
                StartActivity(new Android.Content.Intent(this, typeof(AuthenticationActivity)));
            };

            Profile_VisitSteamProfile.Click += delegate
            {
                // Open WebView with Steam profile
                WebView webView;
                webView = FindViewById<WebView>(Resource.Id.webViewProfile);
                ExtendedWebViewClient webClient = new ExtendedWebViewClient();
                webView.SetWebViewClient(webClient);
                webView.LoadUrl("https://steamcommunity.com/profiles/" + account.SteamId);

                WebSettings webSettings = webView.Settings;
                webSettings.JavaScriptEnabled = true;
            };

        }

        public static void SetProfileAccount(UserAccount Account)
        {
            account = Account;
        }
    }
}