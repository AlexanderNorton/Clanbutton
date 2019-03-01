using System;
using System.Net;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;
using Android.Webkit;
using Android.Runtime;
using Android.Views;
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
        private static TextView Profile_Followers;
        private static ImageView Profile_EditButton;
        private static ImageView Profile_LogoutButton;
        private static ImageView Profile_VisitSteamProfile;
        private static ImageView Profile_SaveChanges;
        private static ImageView Profile_Follow;
        private static ImageView Profile_Unfollow;
        private static ImageView Profile_Avatar;
		private static EditText Profile_EditDiscord;
		private static EditText Profile_EditTwitch;
		private static EditText Profile_EditOrigin;
		private static TextView Profile_Discord;
		private static TextView Profile_Twitch;
		private static TextView Profile_Origin;
		private static ImageView Logo_Origin;
		private static ImageView Logo_Twitch;
		private static ImageView Logo_Discord;
		private static TextView Current_Game;

        WebView webView;

        private UserAccount uaccount;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set the view to UserProfile.
            SetContentView(Resource.Layout.UserProfile_Layout);

            // Add references to layout variables.
            Profile_Username = FindViewById<TextView>(Resource.Id.profile_username);
            Profile_Followers = FindViewById<TextView>(Resource.Id.profile_followers);
            Profile_EditButton = FindViewById<ImageView>(Resource.Id.profile_edit_button);
            Profile_LogoutButton = FindViewById<ImageView>(Resource.Id.profile_logout_button);
            Profile_VisitSteamProfile = FindViewById<ImageView>(Resource.Id.profile_visit_steam_button);
            Profile_SaveChanges = FindViewById<ImageView>(Resource.Id.edit_profile_savechanges);
            Profile_Follow = FindViewById<ImageView>(Resource.Id.profile_follow_button);
            Profile_Unfollow = FindViewById<ImageView>(Resource.Id.profile_unfollow_button);
            Profile_Avatar = FindViewById<ImageView>(Resource.Id.profile_image);
			Profile_EditOrigin = FindViewById<EditText>(Resource.Id.origin_edit);
			Profile_EditTwitch = FindViewById<EditText>(Resource.Id.twitch_edit);
			Profile_EditDiscord = FindViewById<EditText>(Resource.Id.discord_edit);
			Profile_Discord = FindViewById<TextView>(Resource.Id.profile_discord);
			Profile_Twitch = FindViewById<TextView>(Resource.Id.profile_twitch);
			Profile_Origin = FindViewById<TextView>(Resource.Id.profile_origin);
			Logo_Discord = FindViewById<ImageView>(Resource.Id.Discord);
			Logo_Twitch = FindViewById<ImageView>(Resource.Id.Twitch);
			Logo_Origin = FindViewById<ImageView>(Resource.Id.Origin);
			Current_Game = FindViewById<TextView>(Resource.Id.current_game);


            firebase_database = new DatabaseHandler();
            // Get the account of the current user (not the profile user)
            uaccount = await firebase_database.GetAccountAsync(FirebaseAuth.Instance.CurrentUser.Uid);

            // Download profile picture.
            ExtensionMethods extensionMethods = new ExtensionMethods();
            extensionMethods.DownloadPicture(uaccount.Avatar, Profile_Avatar);

            Profile_VisitSteamProfile.Visibility = Android.Views.ViewStates.Visible;
            Profile_Follow.Visibility = Android.Views.ViewStates.Visible;
            //SET PROFILE DATA
            var followers = await firebase_database.GetUserFollowers(account);
            Profile_Username.Text = account.Username;
            Profile_Followers.Text = $"Followers: {followers.Count}";

            if (account.Discord?.Length > 0 ) {
				Profile_Discord.Text = account.Discord;
				Profile_Discord.Visibility = Android.Views.ViewStates.Visible;
				Logo_Discord.Visibility = Android.Views.ViewStates.Visible;
			}
			if (account.Twitch?.Length > 0)
			{
				Profile_Twitch.Text = account.Twitch;
				Profile_Twitch.Visibility = Android.Views.ViewStates.Visible;
				Logo_Twitch.Visibility = Android.Views.ViewStates.Visible;
			}
			if (account.Origin?.Length > 0)
			{
				Profile_Origin.Text = account.Origin;
				Profile_Origin.Visibility = Android.Views.ViewStates.Visible;
				Logo_Origin.Visibility = Android.Views.ViewStates.Visible;
			}
			Current_Game.Visibility = Android.Views.ViewStates.Gone;
			if (account.PlayingGameName != null && account.PlayingGameName != "")
			{
				// Check if player is currently playing a game and add it as a search option.
				Current_Game.Visibility = Android.Views.ViewStates.Visible;
				Current_Game.Text = $"Currently Playing '{account.PlayingGameName}'";
			}

            if (uaccount.IsFollowing(account))
            {
                Profile_Follow.Visibility = Android.Views.ViewStates.Gone;
                Profile_Unfollow.Visibility = Android.Views.ViewStates.Visible;
            }

            if (account.UserId == FirebaseAuth.Instance.CurrentUser.Uid)
            {
                // If the profile is owned by the user, show the edit and logout buttons.
                Profile_EditButton.Visibility = Android.Views.ViewStates.Visible;
                Profile_LogoutButton.Visibility = Android.Views.ViewStates.Visible;
                Profile_Follow.Visibility = Android.Views.ViewStates.Gone;
                Profile_Unfollow.Visibility = Android.Views.ViewStates.Gone;
            }

            Profile_EditButton.Click += delegate
            {
                // Show the edit bar and save changes button.
                Profile_Follow.Visibility = Android.Views.ViewStates.Gone;
                Profile_Unfollow.Visibility = Android.Views.ViewStates.Gone;
                Profile_Origin.Visibility = Android.Views.ViewStates.Gone;
				Profile_Twitch.Visibility = Android.Views.ViewStates.Gone;
				Profile_Discord.Visibility = Android.Views.ViewStates.Gone;
				Logo_Origin.Visibility = Android.Views.ViewStates.Visible;
				Logo_Twitch.Visibility = Android.Views.ViewStates.Visible;
				Logo_Discord.Visibility = Android.Views.ViewStates.Visible;
				Profile_EditOrigin.Visibility = Android.Views.ViewStates.Visible;
				Profile_EditTwitch.Visibility = Android.Views.ViewStates.Visible;
				Profile_EditDiscord.Visibility = Android.Views.ViewStates.Visible;
				Profile_SaveChanges.Visibility = Android.Views.ViewStates.Visible;
                Profile_EditButton.Visibility = Android.Views.ViewStates.Gone;
				Profile_EditDiscord.Text = account.Discord;
				Profile_EditOrigin.Text = account.Origin;
				Profile_EditTwitch.Text = account.Twitch;
			};

            Profile_SaveChanges.Click += delegate
            {
                // Save the profile changes.
                // Remove edit bar and save changes button.
                SaveProfileChanges();
				ExtensionMethods.OpenUserProfile(account, this);
				Finish();
				

            };

            Profile_Follow.Click += delegate
            {
                // Add the uid to the followers of the current account.
                uaccount.Following.Add(account.UserId);
                Profile_Follow.Visibility = Android.Views.ViewStates.Gone;
                Profile_Unfollow.Visibility = Android.Views.ViewStates.Visible;
                uaccount.Update();
                Profile_Followers.Text = $"Followers: {followers.Count}";
            };

            Profile_Unfollow.Click += delegate
            {
                // Remove the uid from the followers of the current account.
                uaccount.Following.Remove(account.UserId);
                Profile_Unfollow.Visibility = Android.Views.ViewStates.Gone;
                Profile_Follow.Visibility = Android.Views.ViewStates.Visible;
                uaccount.Update();
                Profile_Followers.Text = $"Followers: {followers.Count - 1}";
            };

            Profile_LogoutButton.Click += delegate
            {
                FirebaseAuth.Instance.SignOut();
                StartActivity(new Android.Content.Intent(this, typeof(AuthenticationActivity)));
            };

            Profile_VisitSteamProfile.Click += delegate
            {
				// Open WebView with Steam profile.
				SetContentView(Resource.Layout.WebView_Layout);
                webView = FindViewById<WebView>(Resource.Id.webViewProfile);
				webView.Visibility = ViewStates.Visible;
                ExtendedWebViewClient webClient = new ExtendedWebViewClient();
                webView.SetWebViewClient(webClient);
                webView.LoadUrl("https://steamcommunity.com/profiles/" + account.SteamId);

                WebSettings webSettings = webView.Settings;
                webSettings.JavaScriptEnabled = true;
            };

        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (webView != null)
            {
                if (e.Action == KeyEventActions.Down)
                {
                    if (keyCode == Keycode.Back)
                    {
                        ExtensionMethods.OpenUserProfile(uaccount, this);
                        Finish();
                    }
                }
            }
            else
            {
                Finish();
            }
            return true;
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
				account.Origin = Profile_EditOrigin.Text;
				account.Twitch = Profile_EditTwitch.Text;
				account.Discord = Profile_EditDiscord.Text;

				account.Update();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {e.Message}, {e.Source}, {e.TargetSite}, {e.StackTrace}, {e.HResult}, {e.InnerException}, {e.Data}");
            }
        }
    }
}