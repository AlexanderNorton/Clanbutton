using System.Collections.Generic;

using Android.Support.V7.App;

using Clanbutton.Builders;
using Clanbutton.Activities;
using System;
using System.Net;
using Android.Widget;
using Android.Graphics;

namespace Clanbutton.Core
{
    public class ExtensionMethods : AppCompatActivity
    {
        public static List<UserAccount> accs = new List<UserAccount>();
        public static CacheManager cache_manager;

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
                if (e.Error != null)
                {
                    return;
                }

                Bitmap bm;

                var cached_picture = cache_manager.Get<Bitmap>("profile_image_" + url);
                if (cached_picture != null) 
                {
                    bm = cached_picture;
                }
                else
                {
                    bm = BitmapFactory.DecodeByteArray(e.Result, 0, e.Result.Length);
                    cache_manager.Set("profile_image_" + url, bm, DateTime.Now.AddMinutes(30));
                }


                RunOnUiThread(() =>
                {
                    imageview.SetImageBitmap(bm);
                });
            }
        }

        public static void StartCacheManager()
        {
            cache_manager = new CacheManager();
        }

        public string GetTimeSince(DateTime dateFrom)
        {
            TimeSpan elapsed = DateTime.Now.Subtract(dateFrom);
            string thing;
            if (elapsed.Hours > 24)
            {
                var days = (int) elapsed.TotalDays;
                if (days > 1)
                {
                    return $"{days} days ago";
                }
                return $"{days} day ago.";
            }

            var minutes = (int) elapsed.TotalMinutes;
            if (minutes < 60)
            {
                if (minutes > 1)
                {
                    return $"{minutes} minutes ago.";
                }
                return $"{(int) elapsed.TotalSeconds} seconds ago.";
            }

            var hours = (int) elapsed.TotalHours;
            if (hours > 1)
            {
                return $"{hours} hours ago.";
            }
            return $"{hours} hour ago.";
        }
    }
}