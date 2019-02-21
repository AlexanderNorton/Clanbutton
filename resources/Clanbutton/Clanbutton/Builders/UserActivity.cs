using Clanbutton.Core;

namespace Clanbutton.Builders
{
    public class UserActivity
    {
        public string UserId { get; set; }
        public string ActivityMessage { get; set; }

        public UserActivity() { }

        public UserActivity(string userid, string activity_message)
        {
            UserId = userid;
            ActivityMessage = activity_message;
        }

        public void Create()
        {
            DatabaseHandler firebase_database = new DatabaseHandler();
            firebase_database.CreateActivity(this);
        }
    }
}