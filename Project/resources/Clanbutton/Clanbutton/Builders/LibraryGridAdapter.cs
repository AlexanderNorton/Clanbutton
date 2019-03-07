using System.Collections.Generic;

using Android.Views;
using Android.Widget;

using Java.Lang;

using Clanbutton.Activities;
using Clanbutton.Core;
using System.Net;

using Android.App;
using Android.Support.V7.App;
using Android.Content;
using Steam.Models.SteamCommunity;

namespace Clanbutton.Builders
{
    [Activity]
    internal class LibraryGridAdapter : BaseAdapter
    {
        private SearchActivity SearchActivity;
        public List<OwnedGameModel> OwnedGames;

        public LibraryGridAdapter(SearchActivity searchactivity, List<OwnedGameModel> ownedgames)
        {
            SearchActivity = searchactivity;
            OwnedGames = ownedgames;
        }

        public override int Count
        {
            get
            {
                return OwnedGames.Count;
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
            View itemView = convertView;

            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater) SearchActivity.BaseContext.GetSystemService(Context.LayoutInflaterService);
                itemView = inflater.Inflate(Resource.Layout.LibraryGrid_Layout, null);
            }

            ImageView imgView = itemView.FindViewById<ImageView>(Resource.Id.imageView);

            ExtensionMethods extensionMethods = new ExtensionMethods();
            extensionMethods.DownloadPicture($"http://media.steampowered.com/steamcommunity/public/images/apps/{OwnedGames[position].AppId}/{OwnedGames[position].ImgLogoUrl}.jpg", imgView);

            imgView.Click += delegate
            {
                SearchActivity.SearchContent.Text = OwnedGames[position].Name;
            };

            return itemView;
        }
    }
}