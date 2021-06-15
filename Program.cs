using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordVideoBot.Models;
using DiscordVideoBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordVideoBot
{
    class Program
    {
        private IConfigurationRoot _config;

        private static void Main() => new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("BotSettings.json");
            _config = builder.Build();

            var provider = ConfigureServices();

            await provider.GetRequiredService<StartupService>().StartAsync().ConfigureAwait(false);
            provider.GetRequiredService<CommandService>();
            provider.GetRequiredService<LoggingService>();
            var discord = provider.GetRequiredService<DiscordSocketClient>();
            var vlcPath = _config["VlcInstallPath"];

            while (discord.CurrentUser == null)
            {
                Console.WriteLine("Discord user connection pending ...");
                await Task.Delay(5000).ConfigureAwait(false);
            }

            while (discord.ConnectionState != ConnectionState.Connected)
            {
                Console.WriteLine("Discord user connection pending ...");
                await Task.Delay(5000).ConfigureAwait(false);
            }

            Console.WriteLine($"Discord user connected: {discord.CurrentUser.Username}");
            Console.WriteLine("Launching instance of VLC Player. Once launched, begin streaming it to the voice channel of your choice.");

            if (string.IsNullOrEmpty(vlcPath) || !File.Exists(vlcPath))
            {
                Console.WriteLine("Please validate your VlcInstallPath setting in your configuration, and make sure the path/file exists.");
                return;
            }

            Process.Start(vlcPath);

            await Task.Delay(-1).ConfigureAwait(false);
        }

        public IServiceProvider ConfigureServices()
        {
            var socketConfig = new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Error,
                AlwaysDownloadUsers = true
            };

            var services = new ServiceCollection()
                .AddSingleton(new DiscordSocketClient(socketConfig))
                .AddSingleton(new Discord.Commands.CommandService())
                .AddSingleton<CommandService>()
                .AddSingleton<LoggingService>()
                .AddSingleton<StartupService>()
                .AddSingleton<Random>();

            services.Configure<BotSettings>(_config);

            return services.BuildServiceProvider();
        }
    }
}
