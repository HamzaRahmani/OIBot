using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKillboard
{
    public class KillMailRequest
    {
        public FetchType FetchType { get; set; }

        public MailType MailType { get; set; }

        public InfoModifiers InfoModifiers { get; set; }

        public bool OrderAscending { get; set; }

        #region Entity Types

        public IList<long> Characters { get; } = new List<long>();

        public IList<long> Corporations { get; } = new List<long>();

        public IList<long> Alliances { get; } = new List<long>();

        public IList<long> Factions { get; } = new List<long>();

        public IList<long> ShipTypes { get; } = new List<long>();

        public IList<long> Groups { get; } = new List<long>();

        public IList<long> SolarSystems { get; } = new List<long>();

        public IList<long> Regions { get; } = new List<long>();

        public IList<long> Wars { get; } = new List<long>();

        #endregion

        #region Range Modifiers

        public int Limit { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public long BeforeKillId { get; set; }

        public long AfterKillId { get; set; }

        public long PastSeconds { get; set; }

        public long KillId { get; set; }

        #endregion

        public string GetRequestString()
        {
            var sb = new StringBuilder();

            switch (MailType)
            {
                case MailType.Kill:
                    sb.Append("kills/");
                    break;
                case MailType.Loss:
                    sb.Append("losses/");
                    break;
            }

            if (FetchType.HasFlag(FetchType.FinalBlow))
                sb.Append("finalblow-only/");
            if (FetchType.HasFlag(FetchType.Solo))
                sb.Append("solo/");
            if (FetchType.HasFlag(FetchType.WSpace))
                sb.Append("w-space/");

            if (InfoModifiers.HasFlag(InfoModifiers.NoItems))
                sb.Append("no-items/");
            if (InfoModifiers.HasFlag(InfoModifiers.NoAttackers))
                sb.Append("no-attackers/");
            if (InfoModifiers.HasFlag(InfoModifiers.ZkbOnly))
                sb.Append("zkbOnly/");

            if (OrderAscending)
                sb.Append("/orderDirection/asc/");

            if (Characters.Count > 0)
                sb.Append($"characterID/{string.Join(",", Characters)}/");
            if (Corporations.Count > 0)
                sb.Append($"corporationID/{string.Join(",", Corporations)}/");
            if (Alliances.Count > 0)
                sb.Append($"allianceID/{string.Join(",", Alliances)}/");
            if (Factions.Count > 0)
                sb.Append($"factionID/{string.Join(",", Factions)}/");
            if (ShipTypes.Count > 0)
                sb.Append($"shipTypeID/{string.Join(",", ShipTypes)}/");
            if (Groups.Count > 0)
                sb.Append($"groupID/{string.Join(",", Groups)}/");
            if (SolarSystems.Count > 0)
                sb.Append($"solarSystemID/{string.Join(",", SolarSystems)}/");
            if (Regions.Count > 0)
                sb.Append($"regionID/{string.Join(",", Regions)}/");
            if (Wars.Count > 0)
                sb.Append($"warID/{string.Join(",", Wars)}/");

            if (Limit > 0)
                sb.Append($"limit/{Limit}/");
            if (StartTime != default(DateTime))
                sb.Append($"startTime/{StartTime:yyyyMMddHHmm}/");
            if (EndTime != default(DateTime))
                sb.Append($"endTime/{EndTime:yyyyMMddHHmm}/");
            if (Year > 0)
                sb.Append($"year/{Year}/");
            if (Month > 0)
                sb.Append($"month/{Month}/");
            if (AfterKillId > 0)
                sb.Append($"afterKillID/{AfterKillId}/");
            if (BeforeKillId > 0)
                sb.Append($"beforeKillID/{BeforeKillId}/");
            if (PastSeconds > 0)
                sb.Append($"pastSeconds/{PastSeconds}/");

            return sb.ToString();
        }
    }

    public enum MailType
    {
        Both,
        Kill,
        Loss
    }

    [Flags]
    public enum FetchType
    {
        WSpace = 1,
        Solo = 2,
        FinalBlow = 4
    }

    [Flags]
    public enum InfoModifiers
    {
        NoItems = 1,
        NoAttackers = 2,
        ZkbOnly = 4
    }
}
