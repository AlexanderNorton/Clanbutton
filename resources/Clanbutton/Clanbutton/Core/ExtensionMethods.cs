using System.Collections.Generic;

using Android.Support.V7.App;

using Clanbutton.Builders;
using Clanbutton.Activities;
using System;

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

        internal static void CreateBeacon(UserAccount uaccount, string text)
        {
            // Create the beacon activity.
            new UserActivity(uaccount.UserId, uaccount.Username, $"Deployed a beacon and wants to play '{text}'", uaccount.Avatar).Create();
            // Send a notification to all followers.
        }
    }
}