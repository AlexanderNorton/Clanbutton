using Clanbutton.Core;
using System;

namespace Clanbutton.Builders
{
    public class UserActivity
    {
        public string ActivityMessage { get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; }
        public string UserId { get; set; }
        public string GameName { get; set; }
        public DateTime CreationDate { get; set; }

        public UserActivity() { }

        public UserActivity(string userid, string username, string activity_message, string profile_picture, string game_name)
        {
            Username = username;
            ActivityMessage = activity_message;
            ProfilePicture = profile_picture;
            UserId = userid;
            GameName = game_name;
            CreationDate = DateTime.Now;

        }

        public void Create()
        {
            DatabaseHandler firebase_database = new DatabaseHandler();
            firebase_database.CreateActivity(this);
        }
    }
}