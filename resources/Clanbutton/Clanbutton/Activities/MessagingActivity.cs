#region usings
using System.Collections.Generic;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;
using Android.Runtime;

using Firebase.Database;
using Firebase.Auth;
using Firebase.Xamarin.Database;

using Clanbutton.Builders;
using Clanbutton.Activities;
using Clanbutton.Core;
#endregion

namespace Clanbutton
{
    [Activity(Label = "Clanbutton", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MessagingActivity : AppCompatActivity, IValueEventListener
    {
        private FirebaseClient firebase;

        private List<MessageContent> lstMessage = new List<MessageContent>();
        private ListView lstChat;
        private EditText edtChat;
        private Button sendButton;

        public int MyResultCode = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Messaging_Layout);

            firebase = new FirebaseClient(GetString(Resource.String.firebase_database_url));
            FirebaseDatabase.Instance.GetReference("chats").AddValueEventListener(this);

            sendButton = FindViewById<Button>(Resource.Id.sendbutton);
            edtChat = FindViewById<EditText>(Resource.Id.input);
            lstChat = FindViewById<ListView>(Resource.Id.list_of_messages);

            sendButton.Click += delegate
            {
                PostMessage();
            };

            if (FirebaseAuth.Instance.CurrentUser == null)
            {
                StartActivityForResult(new Intent(this, typeof(AuthenticationActivity)), MyResultCode);
            }
            else
            {
                Toast.MakeText(this, "Welcome " + FirebaseAuth.Instance.CurrentUser.Email, ToastLength.Short).Show();
                DisplayChatMessage();
            }

        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        private async void PostMessage()
        {
            FirebaseUser user = FirebaseAuth.Instance.CurrentUser;
            UserAccount Account = await ExtensionMethods.GetAccountAsync(user.Uid.ToString(), firebase);

            var GameSearches = await firebase.Child("gamesearches").OnceAsync<GameSearch>();
            var CurrentGameSearch = "";
            foreach (var game in GameSearches)
            {
                if (game.Object.UserId == Account.UserId)
                {
                    CurrentGameSearch = game.Object.GameName;
                }
            }
            var Items = await firebase.Child("chats").PostAsync(new MessageContent(FirebaseAuth.Instance.CurrentUser.Email, edtChat.Text, CurrentGameSearch));
            edtChat.Text = "";
        }

        public void OnCancelled(DatabaseError error)
        {

        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            DisplayChatMessage();
        }

        private async void DisplayChatMessage()
        {
            lstMessage.Clear();
            FirebaseUser user = FirebaseAuth.Instance.CurrentUser;
            UserAccount Account = await ExtensionMethods.GetAccountAsync(user.Uid.ToString(), firebase);

            var GameSearches = await firebase.Child("gamesearches").OnceAsync<GameSearch>();
            var CurrentGameSearch = "";
            foreach(var game in GameSearches)
            {
                if (game.Object.UserId == Account.UserId)
                {
                    CurrentGameSearch = game.Object.GameName;
                }
            }

            var items = await firebase.Child("chats").OnceAsync<MessageContent>();
            foreach (var item in items)
            {
                if (CurrentGameSearch == item.Object.Game)
                {
                    lstMessage.Add(item.Object);
                    ListViewAdapter.ListViewAdapter adapter = new ListViewAdapter.ListViewAdapter(this, lstMessage);
                    lstChat.Adapter = adapter;
                }
            }
        }
    }
}


