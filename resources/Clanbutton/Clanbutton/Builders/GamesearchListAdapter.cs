using System.Collections.Generic;

using Android.Views;
using Android.Widget;

using Java.Lang;

using Clanbutton.Activities;
using Clanbutton.Core;
using System.Net;

using Android.App;
using Android.Support.V7.App;

namespace Clanbutton.Builders
{
    [Activity]
    internal class GamesearchListAdapter : BaseAdapter
    {
        private SearchActivity searchActivity;
        public List<GameSearch> lstGamesearch;

        public GamesearchListAdapter(SearchActivity SearchActivity, List<GameSearch> lstGamesearch)
        {
            this.searchActivity = SearchActivity;
            this.lstGamesearch = lstGamesearch;
        }

        public override int Count
        {
            get
            {
                return lstGamesearch.Count;
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
            LayoutInflater inflater = (LayoutInflater)searchActivity.BaseContext.GetSystemService(Android.Content.Context.LayoutInflaterService);
            View itemView = inflater.Inflate(Resource.Layout.Gamesearch_ListItem, null);

            TextView gamesearch_user;
            ImageView profile_picture;
            gamesearch_user = itemView.FindViewById<TextView>(Resource.Id.gamesearch_user);
            profile_picture = itemView.FindViewById<ImageView>(Resource.Id.gamesearch_avatar);

            ExtensionMethods extensionMethods = new ExtensionMethods();
            extensionMethods.DownloadPicture(lstGamesearch[position].ProfilePicture, profile_picture);
            
            gamesearch_user.Text = lstGamesearch[position].Username;

            profile_picture.Click += delegate
            {
                OpenProfile(lstGamesearch[position].UserId);
                searchActivity.gamesearches_reference.RemoveEventListener(searchActivity);
            };
            return itemView;
        }

        public async void OpenProfile(string userId)
        {
            DatabaseHandler firebase_database = new DatabaseHandler();
            UserAccount account = await firebase_database.GetAccountAsync(userId);
            ExtensionMethods.OpenUserProfile(account, searchActivity);
        }
    }
}