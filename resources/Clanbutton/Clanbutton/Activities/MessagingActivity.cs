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
        public static UserAccount account;
        public static GameSearch CurrentGameSearch;

        private List<MessageContent> lstMessage = new List<MessageContent>();
        private ListView lstChat;
        private EditText edtChat;
        private Button sendButton;

        public DatabaseReference messaging_reference;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Messaging_Layout);

            firebase_database = new DatabaseHandler();

            FirebaseDatabase.Instance.GetReference("chats").Child(CurrentGameSearch.GameName).AddValueEventListener(this);
            user = FirebaseAuth.Instance.CurrentUser;

            sendButton = FindViewById<Button>(Resource.Id.sendbutton);
            edtChat = FindViewById<EditText>(Resource.Id.input);
            lstChat = FindViewById<ListView>(Resource.Id.list_of_messages);

            messaging_reference = FirebaseDatabase.Instance.GetReference("chats").Child(CurrentGameSearch.GameName);
            messaging_reference.AddValueEventListener(this);

            sendButton.Click += delegate
            {
                PostMessage();
            };
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            messaging_reference.AddValueEventListener(this);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        private void PostMessage()
        {
            MessageContent message = new MessageContent(CurrentGameSearch.Username, edtChat.Text, CurrentGameSearch.GameName, CurrentGameSearch.UserId, CurrentGameSearch.ProfilePicture);
            firebase_database.PostChatMessage(message);
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
            var items = await firebase_database.GetAllChatMessages(CurrentGameSearch.GameName);

            foreach (var item in items)
            {
                if (CurrentGameSearch.GameName == item.Object.Game)
                {
                    lstMessage.Add(item.Object);
                    ListViewAdapter.ListViewAdapter adapter = new ListViewAdapter.ListViewAdapter(this, lstMessage);
                    lstChat.Adapter = adapter;
                }
            }
        }
    }
}

