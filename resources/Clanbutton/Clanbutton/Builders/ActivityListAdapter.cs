using System.Collections.Generic;

using Android.Views;
using Android.Widget;

using Java.Lang;

using Clanbutton.Activities;

namespace Clanbutton.Builders
{
    internal class ActivityListAdapter : BaseAdapter
    {
        private SearchActivity mainActivity;
        private List<UserActivity> lstActivity;

        public ActivityListAdapter(SearchActivity SearchingActivity, List<UserActivity> lstActivity)
        {
            this.mainActivity = SearchingActivity;
            this.lstActivity = lstActivity;
        }

        public override int Count
        {
            get
            {
                return lstActivity.Count;
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
            View itemView = inflater.Inflate(Resource.Layout.Activity_ListItem, null);

            TextView activity_user, activity_content;
            activity_user = itemView.FindViewById<TextView>(Resource.Id.activity_user);
            activity_content = itemView.FindViewById<TextView>(Resource.Id.activity_message);

            activity_user.Text = lstActivity[position].UserId;
            activity_content.Text = lstActivity[position].ActivityMessage;

            return itemView;
        }
    }
}