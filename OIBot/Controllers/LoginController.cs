using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OIBot.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Callback(string code, string state)
        {
            var token = Program.EveClient.AuthorizeAsync(code).Result;
            if (token != null)
            {
                var id = Program.EveClient.VerifyToken(token.AccessToken).Result.CharacterId;
                var character = Program.EveClient.Characters.Get(id).Result;
                var corp = Program.EveClient.Corporations.Get(character.CorporationId).Result;
                var icons = Program.EveClient.Characters.GetPortrait(id).Result;

                var nickname = $"[{corp.Ticker}] {character.Name}";
                ViewData["name"] = nickname;
                ViewData["portrait"] = icons.Px256;

                try
                {
                    var decryptedId = Program.Encryptor.Decrypt(state);
                    ulong.TryParse(decryptedId, out ulong discordId);
                    var guild = Program.DiscordClient.GetGuild(341245386641375232);
                    var user = guild.GetUser(discordId);
                    if (corp.Id == 132013516) //PGGB
                    {
                        var corpRole = guild.Roles.First(r => r.Name == "Corp Members");
                        user.AddRoleAsync(corpRole);
                    }
                    user.ModifyAsync(p => p.Nickname = nickname);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
            return View();
        }
    }
}
