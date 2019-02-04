using Firebase.Auth;
using Firebase.Database;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using System.Collections;

using Clanbutton.Core;

using Steam.Models.SteamCommunity;
using System.Threading.Tasks;

namespace Clanbutton.Builders
{
    public class UserAccount
    {
        public SteamClient client = new SteamClient();
        public string UserId { get; set; }
        public ulong SteamId { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public string Username { get; set; }
        public string Avatar { get; set; }
        public string PlayingGameName { get; set; }
        public string CountryCode { get; set; }

        private ArrayList PastGameSearches = new ArrayList();

        public string CurrentGameSearch { get; set; }

        public UserAccount() { }

        public UserAccount(string userid, ulong steamid, string email)
        {
            UserId = userid;
            SteamId = steamid;
            Email = email;
            Username = "Guest";
        }

        public async void Update(FirebaseClient firebase)
        {
            //Delete current account
            string userid = FirebaseAuth.Instance.CurrentUser.Uid;
            var accounts = await firebase.Child("accounts").OnceAsync<UserAccount>();
            string key = "";

            foreach(var acc in accounts)
            {
                if (acc.Object.UserId == userid)
                {
                    key = acc.Key;
                }
            }
            await firebase.Child("accounts").Child(key).PutAsync(this);

            //Add 'new' account.
        }

        public async Task FillSteamData()
        {
            PlayerSummaryModel SteamAccountModel = await client.GetPlayerSummaryAsync(SteamId);

            Username = SteamAccountModel.Nickname;
            Avatar = SteamAccountModel.AvatarUrl;
            PlayingGameName = SteamAccountModel.PlayingGameName;
            CountryCode = SteamAccountModel.CountryCode;
        }
    }
}