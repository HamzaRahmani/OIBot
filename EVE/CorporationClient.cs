using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EveESI
{
    public class CorporationClient
    {
        private EsiClient _client;

        internal CorporationClient(EsiClient client)
        {
            _client = client;
        }

        public async Task<string> GetName(long corporationId)
        {
            var names = await GetNames(corporationId).ConfigureAwait(false);
            return names[0].Name;
        }

        public Task<CorporationInfo[]> GetNames(params long[] corporationIds)
        {
            return _client.GetAsync<CorporationInfo[]>($"corporations/names/?corporation_ids={string.Join(",", corporationIds)}");
        }

        public Task<long[]> GetNpcCorps()
        {
            return _client.GetAsync<long[]>("corporations/npccorps/");
        }

        public async Task<CorporationInfo> Get(long corporationId)
        {
            var info = await _client.GetAsync<CorporationInfo>($"corporations/{corporationId}").ConfigureAwait(false);
            info.Id = corporationId;
            return info;
        }

        public Task<IconInfo> GetIcons(long corporationId)
        {
            return _client.GetAsync<IconInfo>($"corporations/{corporationId}/icons/");
        }

        public Task<AllianceMembershipInfo[]> GetAllianceHistory(long corporationId)
        {
            return _client.GetAsync<AllianceMembershipInfo[]>($"corporations/{corporationId}/alliancehistory/");
        }

        public Task<CharacterInfo[]> GetMembers(long corporationId)
        {
            return _client.GetAsync<CharacterInfo[]>($"corporations/{corporationId}/members/");
        }
    }

    public struct CorporationInfo
    {
        [JsonProperty("corporation_id")]
        public long Id { get; set; }

        [JsonProperty("corporation_name")]
        public string Name { get; set; }

        [JsonProperty("alliance_id")]
        public long AllianceId { get; set; }

        [JsonProperty("ceo_id")]
        public long CeoId { get; set; }

        [JsonProperty("corporation_description")]
        public string Description { get; set; }

        [JsonProperty("creation_date")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("creator_id")]
        public long CreatorId { get; set; }

        [JsonProperty("member_count")]
        public long MemberCount { get; set; }

        [JsonProperty("tax_rate")]
        public float TaxRate { get; set; }

        [JsonProperty("ticker")]
        public string Ticker { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public struct AllianceMembershipInfo
    {
        [JsonProperty("record_id")]
        public long Id { get; set; }

        [JsonProperty("alliance_id")]
        public long AllianceId { get; set; }

        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; }
    }

    public struct CharacterInfo
    {
        [JsonProperty("character_id")]
        public long Id { get; set; }

        [JsonProperty("ancestry_id")]
        public int AncestryId { get; set; }

        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }

        [JsonProperty("bloodline_id")]
        public int BloodlineId { get; set; }

        [JsonProperty("corporation_id")]
        public long CorporationId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("race_id")]
        public int RaceId { get; set; }
    }
}
