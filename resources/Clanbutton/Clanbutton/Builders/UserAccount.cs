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
		public string Discord { get; set; }
		public string Twitch { get; set; }
		public string Origin { get; set; }
        public ArrayList Following { get; set; }

		private ArrayList PastGameSearches = new ArrayList();

        public string CurrentGameSearch { get; set; }

        public UserAccount()
        {
            Following = new ArrayList();
        }

        public UserAccount(string userid, ulong steamid, string email)
        {
            UserId = userid;
            SteamId = steamid;
            Email = email;
            Username = "Guest";
            Discord = "";
            Twitch = "";
            Origin = "";
            Following = new ArrayList();
        }

        public async void Update()
        {
            string userid = FirebaseAuth.Instance.CurrentUser.Uid;
            DatabaseHandler firebase_database = new DatabaseHandler();
            var accounts = await firebase_database.GetAllAccounts();
            string key = "";

            foreach(var acc in accounts)
            {
                if (acc.Object.UserId == userid)
                {
                    key = acc.Key;
                }
            }
            firebase_database.SaveAccount(this, key);
        }

        public async Task FillSteamData()
        {
            PlayerSummaryModel SteamAccountModel = await client.GetPlayerSummaryAsync(SteamId);

            Username = SteamAccountModel.Nickname;
            Avatar = SteamAccountModel.AvatarFullUrl;
            PlayingGameName = SteamAccountModel.PlayingGameName;
            CountryCode = SteamAccountModel.CountryCode;
        }

        public bool IsFollowing(UserAccount account)
        {
            if (Following == null) { return false; }
            if (Following.Contains(account.UserId)) { return true; }
            else { return false; }
        }
    }
}