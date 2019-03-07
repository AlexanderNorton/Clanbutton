using Clanbutton.Core;
using System;

namespace Clanbutton.Builders
{
    public class Beacon
    {
        public string UserId { get; set; }
        public string GameName { get; set; }
        public DateTime CreationTime { get; set; }

        public Beacon() { }

        public Beacon(string userid, string gamename, DateTime creationtime)
        {
            UserId = userid;
            GameName = gamename;
            CreationTime = creationtime;
        }

        public void Create()
        {
            DatabaseHandler firebase_database = new DatabaseHandler();
            firebase_database.CreateBeacon(this);
        }
    }
}