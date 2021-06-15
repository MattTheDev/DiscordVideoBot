namespace DiscordVideoBot.Models
{
    public class BotSettings
    {
        public Keys KeySettings { get; set; }

        public string VlcInstallPath { get; set; }

        public string Prefix { get; set; }

        public class Keys
        {
            public string DiscordToken { get; set; }
        }
    }
}