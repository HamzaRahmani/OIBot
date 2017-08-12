using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OIBot
{
    public class Config
    {
        public string DiscordToken { get; set; }
        public string EveClientId { get; set; }
        public string EveSecret { get; set; }
    }
}
