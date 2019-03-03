namespace Clanbutton.Builders
{
    public class GameSearch
    {
        public string UserId { get; set; }
        public string GameName { get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; }
        public string CountryPicture { get; set; }

        public GameSearch() { }

        public GameSearch(string gamename, string userid, string username, string profile_picture, string country_picture)
        {
            UserId = userid;
            GameName = gamename;
            Username = username;
            ProfilePicture = profile_picture;
            CountryPicture = country_picture;
        }

    }
}