using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordVideoBot.Models;
using Microsoft.Extensions.Options;

namespace DiscordVideoBot.Services
{
    public class StartupService
    {
        private readonly DiscordSocketClient _discord;
        private readonly Discord.Commands.CommandService _commands;
        private readonly BotSettings _botSettings;
        private readonly IServiceProvider _serviceProvider;

        public StartupService(
            DiscordSocketClient discord,
            Discord.Commands.CommandService commands,
            IOptions<BotSettings> botSettings,
            IServiceProvider serviceProvider)
        {
            _botSettings = botSettings.Value;
            _discord = discord;
            _commands = commands;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync()
        {
            Console.WriteLine("Starting connection to Discord ...");
            var discordToken = _botSettings.KeySettings.DiscordToken;

            if (string.IsNullOrWhiteSpace(discordToken))
            {
                Console.WriteLine("Bot token missing for the `BotSettings.json` file. Please enter the bot token, and restart the service.");

                throw new Exception("Please enter your bot's token into the `BotSettings.json` file found in the applications root directory.");
            }

            await _discord.LoginAsync(TokenType.Bot, discordToken);
            await _discord.StartAsync();

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _serviceProvider);

            Console.WriteLine("Connection to Discord Established ...");
        }
    }
}