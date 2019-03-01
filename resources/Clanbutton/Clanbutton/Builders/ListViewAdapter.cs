using System.Collections.Generic;

using Android.Views;
using Android.Widget;

using Java.Lang;

using Clanbutton.Builders;
using Clanbutton.Core;

namespace Clanbutton.ListViewAdapter
{
    internal class ListViewAdapter : BaseAdapter
    {
        private MessagingActivity mainActivity;
        private List<MessageContent> lstMessage;

        public ListViewAdapter(MessagingActivity MessagingActivity, List<MessageContent> lstMessage)
        {
            this.mainActivity = MessagingActivity;
            this.lstMessage = lstMessage;
        }

        public override int Count
        {
            get
            {
                return lstMessage.Count;
            }
        }

        public override Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)mainActivity.BaseContext.GetSystemService(Android.Content.Context.LayoutInflaterService);
            View itemView = inflater.Inflate(Resource.Layout.List_Item, null);

            TextView message_user, message_time, message_content;
            ImageView message_avatar;
            message_user = itemView.FindViewById<TextView>(Resource.Id.message_user);
            message_content = itemView.FindViewById<TextView>(Resource.Id.message_text);
            message_time = itemView.FindViewById<TextView>(Resource.Id.message_time);
            message_avatar = itemView.FindViewById<ImageView>(Resource.Id.message_avatar);

            message_user.Text = lstMessage[position].Email;
            message_time.Text = lstMessage[position].Time;
            message_content.Text = lstMessage[position].Message;

            ExtensionMethods extensionMethods = new ExtensionMethods();
            extensionMethods.DownloadPicture(lstMessage[position].ProfilePicture, message_avatar);

            message_avatar.Click += delegate
            {
                OpenProfile(lstMessage[position].UserId);
            };


            return itemView;
        }

        public async void OpenProfile(string userId)
        {
            DatabaseHandler firebase_database = new DatabaseHandler();
            UserAccount account = await firebase_database.GetAccountAsync(userId);
            ExtensionMethods.OpenUserProfile(account, mainActivity);
            mainActivity.messaging_reference.RemoveEventListener(mainActivity);
        }
    }
}