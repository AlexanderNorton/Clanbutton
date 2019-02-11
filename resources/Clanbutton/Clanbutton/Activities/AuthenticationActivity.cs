using System;
using System.Web;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;
using Android.Gms.Tasks;

using Firebase.Auth;
using Firebase.Database;
using Firebase.Xamarin.Database;

using Clanbutton.Builders;
using Clanbutton.Core;

using Android.Webkit;
using Android.Graphics;

using SteamWebAPI2.Interfaces;

namespace Clanbutton.Activities
{
    [Activity(Label = "Clanbutton", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class AuthenticationActivity : AppCompatActivity, IOnCompleteListener
    {

        ulong UserId;
        FirebaseAuth auth;

        public async void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                FirebaseClient firebase = new FirebaseClient(GetString(Resource.String.firebase_database_url));
                var AccountCreation = await firebase.Child("accounts").PostAsync(new UserAccount(auth.CurrentUser.Uid.ToString(), UserId, auth.CurrentUser.Email));

                Toast.MakeText(this, "Account created. Welcome to the Clanbutton.", ToastLength.Short).Show();

                StartActivity(new Android.Content.Intent(this, typeof(SearchActivity)));
            }

            else
            {
                Toast.MakeText(this, $"{task.Exception.Message}.", ToastLength.Long).Show();
                return;
            }
        }

        public async void SteamAuth(ulong userid)
        {
            UserId = userid;
            auth = FirebaseAuth.Instance;
            if (await ExtensionMethods.AccountExistsAsync(UserId.ToString(), new FirebaseClient(GetString(Resource.String.firebase_database_url))))
            {
                auth.SignInWithEmailAndPassword($"{UserId.ToString()}@clanbutton.com", "nopass");
                StartActivity(new Android.Content.Intent(this, typeof(SearchActivity)));
            }
            else
            {
                auth.CreateUserWithEmailAndPassword($"{UserId.ToString()}@clanbutton.com", "nopass").AddOnCompleteListener(this);
            }
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Authentication_Layout);

            FirebaseUser user = FirebaseAuth.Instance.CurrentUser;
            if (user != null)
            {
                //User is signed in already.
                StartActivity(new Android.Content.Intent(this, typeof(SearchActivity)));
                return;
            }

            var btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            string steam_url = "https://steamcommunity.com/openid/login?openid.claimed_id=http://specs.openid.net/auth/2.0/identifier_select&openid.identity=http://specs.openid.net/auth/2.0/identifier_select&openid.mode=checkid_setup&openid.ns=http://specs.openid.net/auth/2.0&openid.realm=https://clanbutton&openid.return_to=https://clanbutton/signin/";
			
            btnLogin.Click += delegate
            {
				var btnLogo = FindViewById<Button>(Resource.Id.btnLogo);
				btnLogin.Visibility = Android.Views.ViewStates.Gone;
				btnLogo.Visibility = Android.Views.ViewStates.Gone;
                WebView webView;
                webView = FindViewById<WebView>(Resource.Id.webView);
                ExtendedWebViewClient webClient = new ExtendedWebViewClient();
                webClient.steamAuthentication = this;
                webView.SetWebViewClient(webClient);
                webView.LoadUrl(steam_url);

                WebSettings webSettings = webView.Settings;
                webSettings.JavaScriptEnabled = true;
            };

        }
    }

    internal class ExtendedWebViewClient : WebViewClient
    {
        public AuthenticationActivity steamAuthentication;



        public override async void OnPageStarted(WebView view, string url, Bitmap favicon)
        {
            Uri Url = new Uri(url);

            if (Url.Authority.Equals("clanbutton"))
            {
                Uri userAccountUrl = new Uri(HttpUtility.ParseQueryString(Url.Query).Get("openid.identity"));
                ulong SteamUserId = ulong.Parse(userAccountUrl.Segments[userAccountUrl.Segments.Length - 1]);
                steamAuthentication.SteamAuth(SteamUserId);
                view.StopLoading();
            };
        }
    }
}