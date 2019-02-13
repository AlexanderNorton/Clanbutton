using System.Collections.Generic;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;
using Android.Runtime;

using Firebase.Database;
using Firebase.Auth;

using Clanbutton.Builders;
using Clanbutton.Core;

namespace Clanbutton
{
    [Activity(Label = "Clanbutton", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MessagingActivity : AppCompatActivity, IValueEventListener
    {
        private DatabaseHandler firebase_database;
        private FirebaseUser user;
        private UserAccount account;

        private List<MessageContent> lstMessage = new List<MessageContent>();
        private ListView lstChat;
        private EditText edtChat;
        private Button sendButton;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Messaging_Layout);

            firebase_database = new DatabaseHandler();

            FirebaseDatabase.Instance.GetReference("chats").AddValueEventListener(this);
            user = FirebaseAuth.Instance.CurrentUser;
            account = await firebase_database.GetAccountAsync(user.Uid.ToString());

            sendButton = FindViewById<Button>(Resource.Id.sendbutton);
            edtChat = FindViewById<EditText>(Resource.Id.input);
            lstChat = FindViewById<ListView>(Resource.Id.list_of_messages);

            sendButton.Click += delegate
            {
                PostMessage();
            };

            DisplayChatMessage();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        private async void PostMessage()
        {
            var GameSearches = await firebase_database.GetGameSearchesAsync();

            var CurrentGameSearch = "";
            foreach (var game in GameSearches)
            {
                if (game.Object.UserId == account.UserId)
                {
                    CurrentGameSearch = game.Object.GameName;
                }
            }

            firebase_database.PostChatMessage(account.Username, edtChat.Text, CurrentGameSearch);
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

            var GameSearches = await firebase_database.GetGameSearchesAsync();

            var CurrentGameSearch = "";
            foreach(var game in GameSearches)
            {
                if (game.Object.UserId == account.UserId)
                {
                    CurrentGameSearch = game.Object.GameName;
                }
            }

            var items = await firebase_database.GetAllChatMessages();

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


