using System.Collections.Generic;

using Android.Support.V7.App;

using Clanbutton.Builders;
using Clanbutton.Activities;
using System;
using System.Net;
using Android.Widget;

namespace Clanbutton.Core
{
    public class ExtensionMethods : AppCompatActivity
    {
        public static List<UserAccount> accs = new List<UserAccount>();

        public static void OpenUserProfile(UserAccount Account, Android.Content.Context context)
        {
            ProfileActivity.SetProfileAccount(Account);

            context.StartActivity(new Android.Content.Intent(context, typeof(ProfileActivity)));
        }

        public void DownloadPicture(string url, ImageView imageview)
        {
            // Download profile picture.
            WebClient web = new WebClient();
            web.DownloadDataCompleted += new DownloadDataCompletedEventHandler(web_DownloadDataCompleted);
            web.DownloadDataAsync(new Uri(url));

            void web_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
            {
                Android.Graphics.Bitmap bm = Android.Graphics.BitmapFactory.DecodeByteArray(e.Result, 0, e.Result.Length);

                RunOnUiThread(() =>
                {
                    imageview.SetImageBitmap(bm);
                });
            }
        }
    }
}