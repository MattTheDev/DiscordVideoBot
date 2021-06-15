using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using DiscordVideoBot.Models;
using Microsoft.Extensions.Options;

namespace DiscordVideoBot.Services
{
    public class CommandService
    {
        private readonly DiscordSocketClient _discord;
        private readonly Discord.Commands.CommandService _commands;
        private readonly BotSettings _botSettings;
        private readonly IServiceProvider _provider;

        public CommandService(
            DiscordSocketClient discord,
            Discord.Commands.CommandService commands,
            IOptions<BotSettings> botSettings,
            IServiceProvider provider)
        {
            _discord = discord;
            _commands = commands;
            _botSettings = botSettings.Value;
            _provider = provider;
            _discord.MessageReceived += OnMessageReceivedAsync;
        }

        private async Task OnMessageReceivedAsync(SocketMessage s)
        {
            if (
                s is not SocketUserMessage msg ||
                msg.Author.IsBot ||
                msg.Author.IsWebhook)
            {
                return;
            }

            var context = new SocketCommandContext(_discord, msg);

            var prefix = _botSettings.Prefix;
            var argPos = 0;

            if (msg.HasStringPrefix(prefix, ref argPos) ||
                msg.HasMentionPrefix(_discord.CurrentUser, ref argPos))
            {
                await _commands.ExecuteAsync(context, argPos, _provider);
            }
        }
    }
}