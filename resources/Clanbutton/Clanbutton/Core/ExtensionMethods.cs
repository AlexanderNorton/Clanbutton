using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;
using Android.Gms.Tasks;

using Firebase.Auth;
using Firebase.Database;
using Firebase.Xamarin.Database;

using Clanbutton.Builders;
using Clanbutton.Activities;
using System.Threading.Tasks;

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

        public static async Task<bool> AccountExistsAsync(string userId, FirebaseClient firebaseclient)
        {
            var accounts = await firebaseclient.Child("accounts").OnceAsync<UserAccount>();

            foreach (var account in accounts)
            {
                if (account.Object.SteamId.ToString() == userId)
                {
                    return true;
                }
            }
            return false;
        }

        public static async Task<UserAccount> GetAccountAsync(string userId, FirebaseClient firebaseclient)
        {

            var accounts = await firebaseclient.Child("accounts").OnceAsync<UserAccount>();

            foreach (var account in accounts)
            {
                if (account.Object.UserId.ToString() == userId)
                {
                    UserAccount acc = new UserAccount();

                    acc.UserId = account.Object.UserId.ToString();
                    acc.Email = account.Object.Email;
                    acc.SteamId = account.Object.SteamId;
                    acc.About = account.Object.About;
                    await acc.FillSteamData();

                    return acc;
                }
            }
            return null;
        }


    }

}