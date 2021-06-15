using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace DiscordVideoBot.Services
{
    public class LoggingService
    {
        public LoggingService(
            DiscordSocketClient discord,
            Discord.Commands.CommandService commands)
        {
            if (discord == null)
            {
                throw new ArgumentNullException(nameof(discord));
            }

            if (commands == null)
            {
                throw new ArgumentNullException(nameof(commands));
            }

            discord.Log += OnLogAsync;
            commands.Log += OnLogAsync;
        }

        private Task OnLogAsync(LogMessage msg)
        {
            Console.WriteLine($"[{msg.Source}] - [{msg.Severity} - {msg.Message}]");

            return Task.CompletedTask;
        }
    }
}