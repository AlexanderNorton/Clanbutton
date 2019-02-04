using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steam.Models.SteamCommunity;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SteamWebAPI2.Interfaces;

namespace Clanbutton.Core
{
    public class SteamClient
    {
        public SteamClient()
        {
            SteamInterface = new SteamUser("D31F5813BEA5A009B33CF688883F9CD5");
        }

        public SteamUser SteamInterface { get; set; }

        // Obtain userId info from API.
        public async Task<PlayerSummaryModel> GetPlayerSummary(ulong userId)
        {
            var playerSummaryResponse = await SteamInterface.GetPlayerSummaryAsync(userId);
            return playerSummaryResponse.Data;
        }

        public async Task<IReadOnlyCollection<FriendModel>> GetFriendModels(ulong userId)
        {
            var userFriendsResponse = await SteamInterface.GetFriendsListAsync(userId);
            return userFriendsResponse.Data;
        }
    }
}