using System;

namespace Clanbutton.Builders
{
    internal class MessageContent
    {
        public string Email { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }
        public string Game { get; set; }

        public MessageContent() { }
        public MessageContent(string Email, string Message, string Game)
        {
            this.Email = Email;
            this.Message = Message;
            this.Game = Game;
            Time = DateTime.Now.ToString("yyyy-mm-dd- HH:mm:ss");
        }
    }
}