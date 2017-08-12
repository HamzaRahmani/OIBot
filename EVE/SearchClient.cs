using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EveESI
{
    public class SearchClient
    {
        private EsiClient _client;

        internal SearchClient(EsiClient client)
        {
            _client = client;
        }

        public Task<SearchResult> Search(EntityType categories, string search, bool strict)
        {
            var typeParam = Uri.EscapeDataString(categories.ToString().ToLower().Replace(" ", ""));
            var searchParam = Uri.EscapeDataString(search);
            return _client.GetAsync<SearchResult>($"search/?categories={typeParam}&search={searchParam}&strict={strict}");
        }
    }

    public class SearchResult
    {
        [JsonProperty("agent")]
        public long[] Agents { get; set; }

        [JsonProperty("alliance")]
        public long[] Alliances { get; set; }

        [JsonProperty("character")]
        public long[] Characters { get; set; }

        [JsonProperty("constellation")]
        public long[] Constellations { get; set; }

        [JsonProperty("corporation")]
        public long[] Corporations { get; set; }

        [JsonProperty("faction")]
        public long[] Factions { get; set; }

        [JsonProperty("inventorytype")]
        public long[] ItemTypes { get; set; }

        [JsonProperty("region")]
        public long[] Regions { get; set; }

        [JsonProperty("solarsystem")]
        public long[] SolarSystems { get; set; }

        [JsonProperty("wormhole")]
        public long[] Wormholes { get; set; }
    }
}
