using System;
using System.Net;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;
using Android.Webkit;

using Firebase.Auth;

using Clanbutton.Builders;
using Clanbutton.Core;

namespace Clanbutton.Activities
{
    [Activity(Label = "Clanbutton", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class ProfileActivity : AppCompatActivity
    {
        // Initialize the database.
        private DatabaseHandler firebase_database;

        // Initialize layout variables.
        private static UserAccount account;
        private static TextView Profile_Username;
        private static TextView Profile_About;
        private static Button Profile_EditButton;
        private static Button Profile_LogoutButton;
        private static Button Profile_VisitSteamProfile;
        private static Button Profile_SaveChanges;
        private static EditText Profile_EditAbout;
        private static ImageView Profile_Avatar;
 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set the view to UserProfile.
            SetContentView(Resource.Layout.UserProfile_Layout);

            // Add references to layout variables.
            Profile_Username = FindViewById<TextView>(Resource.Id.profile_username);
            Profile_About = FindViewById<TextView>(Resource.Id.profile_about);
            Profile_EditButton = FindViewById<Button>(Resource.Id.profile_edit_button);
            Profile_LogoutButton = FindViewById<Button>(Resource.Id.profile_logout_button);
            Profile_VisitSteamProfile = FindViewById<Button>(Resource.Id.profile_visit_steam_button);
            Profile_SaveChanges = FindViewById<Button>(Resource.Id.edit_profile_savechanges);
            Profile_EditAbout = FindViewById<EditText>(Resource.Id.edit_profile_about);
            Profile_Avatar = FindViewById<ImageView>(Resource.Id.profile_image);

            //SET PROFILE DATA
            Profile_Username.Text = account.Username;
            Profile_About.Text = account.About;

            // Download profile picture.
            WebClient web = new WebClient();
            web.DownloadDataCompleted += new DownloadDataCompletedEventHandler(web_DownloadDataCompleted);
            web.DownloadDataAsync(new Uri(account.Avatar));

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
                        Profile_Avatar.SetImageBitmap(bm);
                    });
                }
            }

            if (account.UserId == FirebaseAuth.Instance.CurrentUser.Uid)
            {
                // If the profile is owned by the user, show the edit and logout buttons.
                Profile_EditButton.Visibility = Android.Views.ViewStates.Visible;
                Profile_LogoutButton.Visibility = Android.Views.ViewStates.Visible;
            }

            Profile_EditButton.Click += delegate
            {
                // Show the edit bar and save changes button.
                Profile_EditAbout.Visibility = Android.Views.ViewStates.Visible;
                Profile_SaveChanges.Visibility = Android.Views.ViewStates.Visible;
                Profile_EditButton.Visibility = Android.Views.ViewStates.Gone;
                Profile_EditAbout.Text = account.About;
            };

            Profile_SaveChanges.Click += delegate
            {
                // Save the profile changes.
                // Remove edit bar and save changes button.
                SaveProfileChanges();
                Profile_EditAbout.Visibility = Android.Views.ViewStates.Gone;
                Profile_SaveChanges.Visibility = Android.Views.ViewStates.Gone;
                Profile_EditButton.Visibility = Android.Views.ViewStates.Visible;

            };

            Profile_LogoutButton.Click += delegate
            {
                FirebaseAuth.Instance.SignOut();
                StartActivity(new Android.Content.Intent(this, typeof(AuthenticationActivity)));
            };

            Profile_VisitSteamProfile.Click += delegate
            {
                // Open WebView with Steam profile.
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
            // Set the profile's account.
            account = Account;
        }

        public void SaveProfileChanges()
        {
            // Save profile changes and update the account.
            try
            {
                account.About = Profile_EditAbout.Text;

                account.Update();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {e.Message}, {e.Source}, {e.TargetSite}, {e.StackTrace}, {e.HResult}, {e.InnerException}, {e.Data}");
            }
        }
    }
}