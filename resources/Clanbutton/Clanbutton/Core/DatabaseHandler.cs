using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;
using Android.Gms.Tasks;
using Clanbutton.Builders;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;

namespace Clanbutton.Core
{
    public class DatabaseHandler
    {
        public FirebaseClient firebase { get; set; }

        public DatabaseHandler()
        {
            firebase = new FirebaseClient("https://clanbutton-f45a0.firebaseio.com/");
        }

        public async Task<bool> AccountExistsAsync(string userId)
        {
            var accounts = await firebase.Child("accounts").OnceAsync<UserAccount>();

            foreach (var account in accounts)
            {
                if (account.Object.SteamId.ToString() == userId)
                {
                    return true;
                }
            }
            return false;
        }

        public async void CreateAccount(string userId, ulong steamId, string userEmail)
        {
            UserAccount account = new UserAccount(userId, steamId, userEmail);
            var AccountCreation = await firebase.Child("accounts").PostAsync(account);
        }

        public async Task<UserAccount> GetAccountAsync(string userId)
        {
            var accounts = await firebase.Child("accounts").OnceAsync<UserAccount>();

            foreach (var account in accounts)
            {
                if (account.Object.UserId.ToString() == userId)
                {
                    UserAccount acc = new UserAccount();

                    acc.UserId = account.Object.UserId.ToString();
                    acc.Email = account.Object.Email;
                    acc.SteamId = account.Object.SteamId;
					acc.Twitch = account.Object.Twitch;
					acc.Discord = account.Object.Discord;
					acc.Origin = account.Object.Origin;
                    await acc.FillSteamData();

                    return acc;
                }
            }
            return null;
        }

        internal async void SaveAccount(UserAccount userAccount, string key)
        {
            await firebase.Child("accounts").Child(key).PutAsync(userAccount);
        }

        internal async Task<IReadOnlyCollection<FirebaseObject<UserAccount>>> GetAllAccounts()
        {
            var accounts = await firebase.Child("accounts").OnceAsync<UserAccount>();

            return accounts;
        }

        internal async void PostGameSearchAsync(GameSearch game)
        {
            await firebase.Child("gamesearches").PostAsync(game);
        }

        internal async Task<IReadOnlyCollection<FirebaseObject<GameSearch>>> GetGameSearchesAsync()
        {
            var gamesearch = await firebase.Child("gamesearches").OnceAsync<GameSearch>();

            return gamesearch;
        }

        internal async void PostChatMessage(string username, string text, string currentGameSearch)
        {
            MessageContent message = new MessageContent(username, text, currentGameSearch);
            await firebase.Child("chats").PostAsync(message);
        }

        internal async Task<IReadOnlyCollection<FirebaseObject<MessageContent>>> GetAllChatMessages()
        {
            var chats = await firebase.Child("chats").OnceAsync<MessageContent>();
            return chats;
        }
    }
}