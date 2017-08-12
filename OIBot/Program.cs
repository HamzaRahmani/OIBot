using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using EveESI;
using Evepraisal;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using ZKillboard;

namespace OIBot
{
    public static class Program
    {
        public static ZKillboardClient ZKill;
        public static DiscordSocketClient DiscordClient;
        public static EsiClient EveClient;
        public static EvepraisalClient Evepraisal;
        public static AesEncryptor Encryptor = new AesEncryptor();

        public static void Main(string[] args)
        {
            //TODO create key for data and use password to decrypt key
            Console.WriteLine("Enter password:");
            var pass = Console.ReadLine();
            var configEncryptor = new AesEncryptor(pass, File.ReadAllText("salt"));

            var config = new Config();
            if (File.Exists("config"))
            {
                config = JsonConvert.DeserializeObject<Config>(configEncryptor.DecryptFromBytes(File.ReadAllBytes("config")));
            }
            else
            {
                Console.WriteLine("Enter Discord token:");
                config.DiscordToken = Console.ReadLine();
                Console.WriteLine("Enter EVE client ID:");
                config.EveClientId = Console.ReadLine();
                Console.WriteLine("Enter EVE client secret:");
                config.EveSecret = Console.ReadLine();

                File.WriteAllBytes("config", configEncryptor.EncryptToBytes(JsonConvert.SerializeObject(config)));
            }
            Console.Clear();

            EveClient = new EsiClient(config.EveClientId, config.EveSecret, new ProductInfoHeaderValue("OIBot", "1.0"));
            ZKill = new ZKillboardClient();
            Evepraisal = new EvepraisalClient();

            RunBot(config.DiscordToken).ConfigureAwait(false);

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .UseUrls("http://jimmacle.com:80")
                .Build();

            host.Run();
        }

        public static async Task RunBot(string token)
        {
            DiscordClient = new DiscordSocketClient();
            await DiscordClient.LoginAsync(TokenType.Bot, token).ConfigureAwait(false);
            await DiscordClient.StartAsync().ConfigureAwait(false);
            await DiscordClient.SetGameAsync("Project Discovery").ConfigureAwait(false);

            DiscordClient.MessageReceived += Client_MessageReceived;
        }

        private static async Task Client_MessageReceived(SocketMessage arg)
        {
            if (!arg.Content.StartsWith("!"))
                return;

            var args = arg.Content.Substring(1, arg.Content.Length - 1).Split(' ');
            if (args.Length == 0)
                return;

            try
            {
                switch (args[0])
                {
                    case "alliance":
                        await SendAllianceInfoAsync(arg.Channel, args[1]).ConfigureAwait(false);
                        break;
                    case "corp":
                        await SendCorpInfoAsync(arg.Channel, args[1]).ConfigureAwait(false);
                        break;
                    case "auth":
                        var encryptedId = Encryptor.Encrypt(arg.Author.Id.ToString());
                        await arg.Channel.SendMessageAsync($"{arg.Author.Mention} Authorization link sent in direct message.").ConfigureAwait(false);
                        await arg.Author.SendMessageAsync(EveClient.GetAuthUrl("http://jimmacle.com/Login/Callback", EsiScope.None, encryptedId)).ConfigureAwait(false);
                        break;
                    case "who":
                        var searchStr = string.Join(" ", args.Skip(1));
                        var result = await EveClient.Search.Search(EntityType.Character, searchStr, false).ConfigureAwait(false);
                        await SendCharacterInfo(arg.Channel, result.Characters.FirstOrDefault().ToString()).ConfigureAwait(false);
                        break;
                    case "appraise":
                        var rawText = arg.Content.Split(new[] {"```"}, StringSplitOptions.RemoveEmptyEntries)[1];
                        var appraisal = await Evepraisal.AppraiseAsync(rawText, Market.Jita).ConfigureAwait(false);
                        await SendAppraisalAsync(arg.Channel, appraisal).ConfigureAwait(false);
                        break;
                    default:
                        await arg.Channel.SendMessageAsync("Unrecognized command.").ConfigureAwait(false);
                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        private static Task SendAppraisalAsync(ISocketMessageChannel channel, Appraisal appraisal)
        {
            var embed = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder().WithName($"Total worth: {appraisal.Totals.Buy} buy, {appraisal.Totals.Sell} sell")
            };

            foreach (var item in appraisal.Items)
            {
                embed.Fields.Add(new EmbedFieldBuilder().WithName($"{item.TypeName} x{item.Quantity}").WithValue($"Buy: {item.Prices.Buy.Average} Sell: {item.Prices.Sell.Average}"));
            }

            return channel.SendMessageAsync("", embed: embed.Build());
        }

        private static async Task SendAllianceInfoAsync(ISocketMessageChannel channel, string arg)
        {
            if (long.TryParse(arg, out long allianceId))
            {
                var alliance = await EveClient.Alliances.Get(allianceId).ConfigureAwait(false);
                var icons = await EveClient.Alliances.GetIcons(allianceId).ConfigureAwait(false);

                var embed = new EmbedBuilder
                {
                    Author = new EmbedAuthorBuilder().WithName(alliance.Name).WithUrl(ZKill.GetAllianceLink(alliance.Id)),
                    ThumbnailUrl = icons.GetSmallest(),
                    Fields = new List<EmbedFieldBuilder>
                    {
                        new EmbedFieldBuilder().WithName("Founded").WithValue(alliance.DateFounded).WithIsInline(true),
                        new EmbedFieldBuilder().WithName("Ticker").WithValue(alliance.Ticker).WithIsInline(true)
                    }
                };

                await channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);
            }
        }

        private static async Task SendCorpInfoAsync(ISocketMessageChannel channel, string arg)
        {
            if (long.TryParse(arg, out long corpId))
            {
                var corp = await EveClient.Corporations.Get(corpId).ConfigureAwait(false);
                var icons = await EveClient.Corporations.GetIcons(corpId).ConfigureAwait(false);

                var embed = new EmbedBuilder
                {
                    Author = new EmbedAuthorBuilder().WithName(corp.Name).WithUrl(ZKill.GetCorpLink(corp.Id)),
                    ThumbnailUrl = icons.GetSmallest(),
                    Fields = new List<EmbedFieldBuilder>
                    {
                        new EmbedFieldBuilder().WithName("Founded").WithValue(corp.CreationDate),
                        new EmbedFieldBuilder().WithName("Ticker").WithValue(corp.Ticker).WithIsInline(true),
                        new EmbedFieldBuilder().WithName("Members").WithValue(corp.MemberCount),
                    }
                };

                await channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);
            }
        }

        private static async Task SendCharacterInfo(ISocketMessageChannel channel, string arg)
        {
            if (long.TryParse(arg, out long charId))
            {
                var character = await EveClient.Characters.Get(charId).ConfigureAwait(false);
                var corp = await EveClient.Corporations.Get(character.CorporationId).ConfigureAwait(false);
                var icons = await EveClient.Characters.GetPortrait(charId).ConfigureAwait(false);

                var embed = new EmbedBuilder
                {
                    Author = new EmbedAuthorBuilder().WithName(character.Name ?? "null").WithUrl(ZKill.GetCharacterLink(character.Id)),
                    ThumbnailUrl = icons.GetSmallest(),
                    Fields = new List<EmbedFieldBuilder>
                    {
                        new EmbedFieldBuilder().WithName("Born").WithValue(character.Birthday.ToString()),
                        new EmbedFieldBuilder().WithName("Corp").WithValue(corp.Name ?? "n/a"),
                    }
                };

                await channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);
            }
        }
    }
}
