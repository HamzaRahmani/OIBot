using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using NLog;

namespace OIBot.Discord
{
    public class DiscordBot
    {
        private static readonly Logger _log = LogManager.GetLogger("Discord");
        private DiscordSocketClient _client;

        public async Task Run(string configFile)
        {
            _log.Info($"Starting bot with config file {configFile}");

            DiscordConfig config;
            if (File.Exists(configFile))
            {
                config = (DiscordConfig)JsonConvert.DeserializeObject(File.ReadAllText(configFile));
            }
            else
            {
                _log.Error($"Config not found, generating default at {configFile}.");
                File.WriteAllText(configFile, JsonConvert.SerializeObject(new DiscordConfig()));
                return;
            }

            _client = new DiscordSocketClient();
            await _client.LoginAsync(TokenType.Bot, config.Token).ConfigureAwait(false);
            await _client.StartAsync().ConfigureAwait(false);
        }
    }
}
