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
    public class ProfileActivity : AppCompatActivity
    {
        private FirebaseClient firebase;

        private static UserAccount account;
        private static TextView Profile_Username;
        private static Button Profile_EditButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserProfile_Layout);

            Profile_Username = FindViewById<TextView>(Resource.Id.profile_username);
            Profile_EditButton = FindViewById<Button>(Resource.Id.profile_edit_button);

            //SET PROFILE DATA
            Profile_Username.Text = account.Username;

            if (account.UserId != FirebaseAuth.Instance.CurrentUser.Uid)
            {
                Profile_EditButton.Visibility = Android.Views.ViewStates.Gone;
            }

            firebase = new FirebaseClient(GetString(Resource.String.firebase_database_url));

            Profile_EditButton.Click += delegate
            {
                StartActivity(new Android.Content.Intent(this, typeof(EditProfileActivity)));
            };

        }

        public static void SetProfileAccount(UserAccount Account)
        {
            account = Account;
        }
    }
}