using System;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;
using Android.Gms.Tasks;

using Firebase.Auth;
using Firebase.Database;
using Firebase.Xamarin.Database;

using Clanbutton.Builders;

namespace Clanbutton.Activities
{
    [Activity(Label = "Clanbutton", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]

    {
        FirebaseAuth auth;
        int MyResultCode = 1;

        public async void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                FirebaseClient firebase = new FirebaseClient(GetString(Resource.String.firebase_database_url));
                var AccountCreation = await firebase.Child("accounts").PostAsync(new UserAccount(auth.CurrentUser.Uid.ToString(), auth.CurrentUser.Email));

                Toast.MakeText(this, "Account created. Welcome to the Clanbutton.", ToastLength.Short).Show();

                StartActivityForResult(new Android.Content.Intent(this, typeof(SearchActivity)), MyResultCode);
            }

            else
            {
                Toast.MakeText(this, $"{task.Exception.Message}.", ToastLength.Long).Show();
                return;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Authentication_Layout);

            auth = FirebaseAuth.Instance;

            var edtEmail = FindViewById<EditText>(Resource.Id.edtEmail);
            var edtPassword = FindViewById<EditText>(Resource.Id.edtPassword);
            var btnRegister = FindViewById<Button>(Resource.Id.btnRegister);

            btnRegister.Click += delegate
            {
                auth.CreateUserWithEmailAndPassword(edtEmail.Text, edtPassword.Text).AddOnCompleteListener(this);
            };

        }
    }
}