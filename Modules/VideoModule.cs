using System.Diagnostics;
using System.Threading.Tasks;
using Discord.Commands;
using DiscordVideoBot.Models;
using Microsoft.Extensions.Options;

namespace DiscordVideoBot.Modules
{
    [Group("video")]
    public class VideoModule : ModuleBase
    {
        private readonly BotSettings _botSettings;

        public VideoModule(IOptions<BotSettings> botSettings)
        {
            _botSettings = botSettings.Value;
        }

        [Command("play", RunMode = RunMode.Async)]
        public async Task VideoPlay([Remainder] string url)
        {
            Process.Start(_botSettings.VlcInstallPath, url);

            await ReplyAsync("Please wait - Video Will Begin Shortly.");
        }
    }
}