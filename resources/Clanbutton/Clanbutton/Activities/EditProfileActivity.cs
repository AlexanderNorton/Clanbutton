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
using Firebase.Xamarin.Database.Query;

namespace Clanbutton.Activities
{
    [Activity(Label = "Clanbutton", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class EditProfileActivity : AppCompatActivity
    {
        private FirebaseClient firebase;
        private Button save_changes_button;
        private EditText edit_about;
        private static TextView Profile_Username;

        public UserAccount Account;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.EditUserProfile_Layout);

                firebase = new FirebaseClient(GetString(Resource.String.firebase_database_url));

                FirebaseUser user = FirebaseAuth.Instance.CurrentUser;
                Account = await ExtensionMethods.GetAccountAsync(user.Uid.ToString(), firebase);

                save_changes_button = FindViewById<Button>(Resource.Id.edit_profile_savechanges);
                edit_about = FindViewById<EditText>(Resource.Id.edit_profile_about);
                Profile_Username = FindViewById<TextView>(Resource.Id.edit_profile_username);

                edit_about.Text = Account.About;
                Profile_Username.Text = Account.Username;

                save_changes_button.Click += delegate
                {
                    SaveProfileChanges();
                };
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {e.Message}, {e.Source}, {e.TargetSite}, {e.StackTrace}, {e.HResult}, {e.InnerException}, {e.Data}");
            }
        }

        public void SaveProfileChanges()
        {
            try
            {
                Account.About = edit_about.Text;

                Account.Update(firebase);

                ExtensionMethods.OpenUserProfile(Account, this);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {e.Message}, {e.Source}, {e.TargetSite}, {e.StackTrace}, {e.HResult}, {e.InnerException}, {e.Data}");
            }
        }

    }
}