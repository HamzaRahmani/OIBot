using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EveESI
{
    public class AllianceClient
    {
        private EsiClient _client;

        internal AllianceClient(EsiClient client)
        {
            _client = client;
        }

        public Task<long[]> GetAll()
        {
            return _client.GetAsync<long[]>("alliances/");
        }

        public async Task<string> GetName(long allianceId)
        {
            var names = await GetNames(allianceId).ConfigureAwait(false);
            return names[0].Name;
        }

        public Task<AllianceInfo[]> GetNames(params long[] allianceIds)
        {
            return _client.GetAsync<AllianceInfo[]>($"alliances/names/?alliance_ids={string.Join(",", allianceIds)}");
        }

        public async Task<AllianceInfo> Get(long allianceId)
        {
            var info = await _client.GetAsync<AllianceInfo>($"alliances/{allianceId}").ConfigureAwait(false);
            info.Id = allianceId;
            return info;
        }

        public Task<long[]> GetCorporations(long allianceId)
        {
            return _client.GetAsync<long[]>($"alliances/{allianceId}/corporations/");
        }

        public Task<IconInfo> GetIcons(long allianceId)
        {
            return _client.GetAsync<IconInfo>($"alliances/{allianceId}/icons/");
        }
    }

    public struct AllianceInfo
    {
        [JsonProperty("alliance_id")]
        public long Id { get; set; }

        [JsonProperty("alliance_name")]
        public string Name { get; set; }

        [JsonProperty("date_founded")]
        public DateTime DateFounded { get; set; }

        [JsonProperty("executor_corp")]
        public long ExecutorCorp { get; set; }

        [JsonProperty("ticker")]
        public string Ticker { get; set; }
    }

    public struct IconInfo
    {
        [JsonProperty("px64x64")]
        public string Px64 { get; set; }

        [JsonProperty("px128x128")]
        public string Px128 { get; set; }

        [JsonProperty("px256x256")]
        public string Px256 { get; set; }

        [JsonProperty("px512x512")]
        public string Px512 { get; set; }

        public string GetSmallest()
        {
            return Px64 ?? Px128 ?? Px256 ?? Px512;
        }

        public string GetLargest()
        {
            return Px512?? Px256 ?? Px128 ?? Px64;
        }
    }
}
