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
using Firebase.Database;
using System.Collections;

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
            var cached_account = ExtensionMethods.cache_manager.Get<UserAccount>("user_account_" + userId);
            if (cached_account != null)
            {
                return cached_account;
            }

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
                    if (account.Object.Following != null) { acc.Following.AddRange(account.Object.Following); } else { acc.Following = new ArrayList(); }
                    await acc.FillSteamData();

                    ExtensionMethods.cache_manager.Set("user_account_" + userId, acc, DateTime.Now.AddSeconds(5));

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

        internal async void RemoveGameSearchAsync(string key)
        {
            await firebase.Child("gamesearches").Child(key).DeleteAsync();
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

        internal async void CreateActivity(UserActivity activity)
        {
            await firebase.Child("activities").PostAsync(activity);
        }

        internal async void CreateBeacon(Beacon beacon)
        {
            var current_beacon = await GetBeaconForUser(beacon.UserId);
            if (current_beacon != null)
            {
                await firebase.Child("beacons").Child(current_beacon.Key).DeleteAsync();
            }
            await firebase.Child("beacons").PostAsync(beacon);
        }

        internal async Task<IReadOnlyCollection<FirebaseObject<Beacon>>> GetAllBeacons()
        {
            var beacons = await firebase.Child("beacons").OnceAsync<Beacon>();
            return beacons;
        }

        internal async Task<FirebaseObject<Beacon>>GetBeaconForUser(string userid)
        {
            var beacons = await GetAllBeacons();
            foreach (var b in beacons)
            {
                if (b.Object.UserId == userid)
                {
                    return b;
                }
            }
            return null;
        }

        internal async Task<IReadOnlyCollection<FirebaseObject<MessageContent>>> GetAllChatMessages()
        {
            var chats = await firebase.Child("chats").OnceAsync<MessageContent>();
            return chats;
        }

        internal async Task<ArrayList> GetUserFollowers(UserAccount account)
        {
            ArrayList followers = new ArrayList();
            var accounts = await GetAllAccounts();
            foreach (var uaccount in accounts)
            {
                if (uaccount.Object.Following == null) { return followers; }
                if (uaccount.Object.Following.Contains(account.UserId)){
                    followers.Add(account.UserId);
                }
            } 
            return followers;
        }

        internal async Task<IReadOnlyCollection<FirebaseObject<UserActivity>>> GetAllActivities()
        {
            var activities = await firebase.Child("activities").OnceAsync<UserActivity>();
            return activities;
        }
    }
}