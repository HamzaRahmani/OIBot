using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace ZKillboard
{
    public class KillMail
    {
        [JsonProperty("killID")]
        public long KillId { get; set; }

        [JsonProperty("solarSystemID")]
        public long SolarSystemId { get; set; }

        [JsonProperty("killTime")]
        public DateTime KillTime { get; set; }

        [JsonProperty("moonID")]
        public long MoonId { get; set; }

        [JsonProperty("victim")]
        public VictimInfo Victim { get; set; }

        [JsonProperty("attackers")]
        public AttackerInfo[] Attackers { get; set; }

        [JsonProperty("items")]
        public ItemInfo[] Items { get; set; }

        public class CharacterInfo
        {
            [JsonProperty("characterId")]
            public long CharacterId { get; set; }

            [JsonProperty("characterName")]
            public string CharacterName { get; set; }

            [JsonProperty("corporationID")]
            public long CorporationId { get; set; }

            [JsonProperty("corporationName")]
            public string CorporationName { get; set; }

            [JsonProperty("allianceID")]
            public long AllianceId { get; set; }

            [JsonProperty("allianceName")]
            public string AllianceName { get; set; }

            [JsonProperty("factionID")]
            public long FactionId { get; set; }

            [JsonProperty("factionName")]
            public string FactionName { get; set; }

            [JsonProperty("shipTypeID")]
            public long ShipTypeId { get; set; }
        }

        public class VictimInfo : CharacterInfo
        {
            [JsonProperty("damageTaken")]
            public long DamageTaken { get; set; }
        }

        public class AttackerInfo : CharacterInfo
        {
            [JsonProperty("securityStatus")]
            public float SecurityStatus { get; set; }

            [JsonProperty("damageDone")]
            public long DamageDone { get; set; }

            [JsonProperty("finalBlow")]
            public bool FinalBlow { get; set; }

            [JsonProperty("weaponTypeID")]
            public long WeaponTypeId { get; set; }
        }

        public class ItemInfo
        {
            [JsonProperty("typeID")]
            public long TypeId { get; set; }

            [JsonProperty("flag")]
            public long Flag { get; set; }

            [JsonProperty("qtyDropped")]
            public long Dropped { get; set; }

            [JsonProperty("qtyDestroyed")]
            public long Destroyed { get; set; }

            [JsonProperty("singleton")]
            public long Singleton { get; set; }
        }
    }
}
