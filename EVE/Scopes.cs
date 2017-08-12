//WARNING: 
//This file is generated automatically! Any modifications will be overwritten.

using System;
using System.Collections.Generic;

namespace EveESI
{
	///<summary>
	///ESI scope flags. Use <see cref="EsiScopeMap"/> to convert to and from strings.
	///</summary>
	[Flags]
	public enum EsiScope : ulong 
	{
		///<summary>
		///No permissions.
		///</summary>
		None = 0,
        ///<summary>
        ///esi-assets.read_assets.v1
        ///</summary>
        AssetsRead                               = 1UL<<0,
        ///<summary>
        ///All Assets permissions
        ///</summary>
        AllAssets = AssetsRead,
        ///<summary>
        ///esi-bookmarks.read_character_bookmarks.v1
        ///</summary>
        BookmarksReadCharacter                   = 1UL<<1,
        ///<summary>
        ///All Bookmarks permissions
        ///</summary>
        AllBookmarks = BookmarksReadCharacter,
        ///<summary>
        ///esi-calendar.read_calendar_events.v1
        ///</summary>
        CalendarReadEvents                       = 1UL<<2,
        ///<summary>
        ///esi-calendar.respond_calendar_events.v1
        ///</summary>
        CalendarRespondEvents                    = 1UL<<3,
        ///<summary>
        ///All Calendar permissions
        ///</summary>
        AllCalendar = CalendarReadEvents | CalendarRespondEvents,
        ///<summary>
        ///esi-characters.read_agents_research.v1
        ///</summary>
        CharactersReadAgentsResearch             = 1UL<<4,
        ///<summary>
        ///esi-characters.read_blueprints.v1
        ///</summary>
        CharactersReadBlueprints                 = 1UL<<5,
        ///<summary>
        ///esi-characters.read_chat_channels.v1
        ///</summary>
        CharactersReadChatChannels               = 1UL<<6,
        ///<summary>
        ///esi-characters.read_contacts.v1
        ///</summary>
        CharactersReadContacts                   = 1UL<<7,
        ///<summary>
        ///esi-characters.read_corporation_roles.v1
        ///</summary>
        CharactersReadCorporationRoles           = 1UL<<8,
        ///<summary>
        ///esi-characters.read_fatigue.v1
        ///</summary>
        CharactersReadFatigue                    = 1UL<<9,
        ///<summary>
        ///esi-characters.read_loyalty.v1
        ///</summary>
        CharactersReadLoyalty                    = 1UL<<10,
        ///<summary>
        ///esi-characters.read_medals.v1
        ///</summary>
        CharactersReadMedals                     = 1UL<<11,
        ///<summary>
        ///esi-characters.read_opportunities.v1
        ///</summary>
        CharactersReadOpportunities              = 1UL<<12,
        ///<summary>
        ///esi-characters.read_standings.v1
        ///</summary>
        CharactersReadStandings                  = 1UL<<13,
        ///<summary>
        ///esi-characters.write_contacts.v1
        ///</summary>
        CharactersWriteContacts                  = 1UL<<14,
        ///<summary>
        ///All Characters permissions
        ///</summary>
        AllCharacters = CharactersReadAgentsResearch | CharactersReadBlueprints | CharactersReadChatChannels | CharactersReadContacts | CharactersReadCorporationRoles | CharactersReadFatigue | CharactersReadLoyalty | CharactersReadMedals | CharactersReadOpportunities | CharactersReadStandings | CharactersWriteContacts,
        ///<summary>
        ///esi-clones.read_clones.v1
        ///</summary>
        ClonesRead                               = 1UL<<15,
        ///<summary>
        ///esi-clones.read_implants.v1
        ///</summary>
        ClonesReadImplants                       = 1UL<<16,
        ///<summary>
        ///All Clones permissions
        ///</summary>
        AllClones = ClonesRead | ClonesReadImplants,
        ///<summary>
        ///esi-contracts.read_character_contracts.v1
        ///</summary>
        ContractsReadCharacter                   = 1UL<<17,
        ///<summary>
        ///All Contracts permissions
        ///</summary>
        AllContracts = ContractsReadCharacter,
        ///<summary>
        ///esi-corporations.read_corporation_membership.v1
        ///</summary>
        CorporationsReadCorporationMembership    = 1UL<<18,
        ///<summary>
        ///esi-corporations.read_structures.v1
        ///</summary>
        CorporationsReadStructures               = 1UL<<19,
        ///<summary>
        ///esi-corporations.write_structures.v1
        ///</summary>
        CorporationsWriteStructures              = 1UL<<20,
        ///<summary>
        ///All Corporations permissions
        ///</summary>
        AllCorporations = CorporationsReadCorporationMembership | CorporationsReadStructures | CorporationsWriteStructures,
        ///<summary>
        ///esi-fittings.read_fittings.v1
        ///</summary>
        FittingsRead                             = 1UL<<21,
        ///<summary>
        ///esi-fittings.write_fittings.v1
        ///</summary>
        FittingsWrite                            = 1UL<<22,
        ///<summary>
        ///All Fittings permissions
        ///</summary>
        AllFittings = FittingsRead | FittingsWrite,
        ///<summary>
        ///esi-fleets.read_fleet.v1
        ///</summary>
        FleetsReadFleet                          = 1UL<<23,
        ///<summary>
        ///esi-fleets.write_fleet.v1
        ///</summary>
        FleetsWriteFleet                         = 1UL<<24,
        ///<summary>
        ///All Fleets permissions
        ///</summary>
        AllFleets = FleetsReadFleet | FleetsWriteFleet,
        ///<summary>
        ///esi-industry.read_character_jobs.v1
        ///</summary>
        IndustryReadCharacterJobs                = 1UL<<25,
        ///<summary>
        ///All Industry permissions
        ///</summary>
        AllIndustry = IndustryReadCharacterJobs,
        ///<summary>
        ///esi-killmails.read_killmails.v1
        ///</summary>
        KillmailsRead                            = 1UL<<26,
        ///<summary>
        ///All Killmails permissions
        ///</summary>
        AllKillmails = KillmailsRead,
        ///<summary>
        ///esi-location.read_location.v1
        ///</summary>
        LocationRead                             = 1UL<<27,
        ///<summary>
        ///esi-location.read_online.v1
        ///</summary>
        LocationReadOnline                       = 1UL<<28,
        ///<summary>
        ///esi-location.read_ship_type.v1
        ///</summary>
        LocationReadShipType                     = 1UL<<29,
        ///<summary>
        ///All Location permissions
        ///</summary>
        AllLocation = LocationRead | LocationReadOnline | LocationReadShipType,
        ///<summary>
        ///esi-mail.organize_mail.v1
        ///</summary>
        MailOrganize                             = 1UL<<30,
        ///<summary>
        ///esi-mail.read_mail.v1
        ///</summary>
        MailRead                                 = 1UL<<31,
        ///<summary>
        ///esi-mail.send_mail.v1
        ///</summary>
        MailSend                                 = 1UL<<32,
        ///<summary>
        ///All Mail permissions
        ///</summary>
        AllMail = MailOrganize | MailRead | MailSend,
        ///<summary>
        ///esi-markets.read_character_orders.v1
        ///</summary>
        MarketsReadCharacterOrders               = 1UL<<33,
        ///<summary>
        ///esi-markets.structure_markets.v1
        ///</summary>
        MarketsStructure                         = 1UL<<34,
        ///<summary>
        ///All Markets permissions
        ///</summary>
        AllMarkets = MarketsReadCharacterOrders | MarketsStructure,
        ///<summary>
        ///esi-planets.manage_planets.v1
        ///</summary>
        PlanetsManage                            = 1UL<<35,
        ///<summary>
        ///All Planets permissions
        ///</summary>
        AllPlanets = PlanetsManage,
        ///<summary>
        ///esi-search.search_structures.v1
        ///</summary>
        SearchStructures                         = 1UL<<36,
        ///<summary>
        ///All Search permissions
        ///</summary>
        AllSearch = SearchStructures,
        ///<summary>
        ///esi-skills.read_skillqueue.v1
        ///</summary>
        SkillsReadSkillqueue                     = 1UL<<37,
        ///<summary>
        ///esi-skills.read_skills.v1
        ///</summary>
        SkillsRead                               = 1UL<<38,
        ///<summary>
        ///All Skills permissions
        ///</summary>
        AllSkills = SkillsReadSkillqueue | SkillsRead,
        ///<summary>
        ///esi-ui.open_window.v1
        ///</summary>
        UiOpenWindow                             = 1UL<<39,
        ///<summary>
        ///esi-ui.write_waypoint.v1
        ///</summary>
        UiWriteWaypoint                          = 1UL<<40,
        ///<summary>
        ///All Ui permissions
        ///</summary>
        AllUi = UiOpenWindow | UiWriteWaypoint,
        ///<summary>
        ///esi-universe.read_structures.v1
        ///</summary>
        UniverseReadStructures                   = 1UL<<41,
        ///<summary>
        ///All Universe permissions
        ///</summary>
        AllUniverse = UniverseReadStructures,
        ///<summary>
        ///esi-wallet.read_character_wallet.v1
        ///</summary>
        WalletReadCharacter                      = 1UL<<42,
        ///<summary>
        ///All permissions
        ///</summary>
        All = AllAssets | AllBookmarks | AllCalendar | AllCharacters | AllClones | AllContracts | AllCorporations | AllFittings | AllFleets | AllIndustry | AllKillmails | AllLocation | AllMail | AllMarkets | AllPlanets | AllSearch | AllSkills | AllUi | AllUniverse,
        
	}

	///<summary>
	///Static class with methods to translate between a collection of ESI scope strings and the <see cref="EsiScope"/> enum.
	///</summary>
	public static class EsiScopeMap
	{
		public static IReadOnlyDictionary<string, EsiScope> Strings { get; } = new Dictionary<string, EsiScope>
		{
            ["esi-assets.read_assets.v1"] = EsiScope.AssetsRead,
            ["esi-bookmarks.read_character_bookmarks.v1"] = EsiScope.BookmarksReadCharacter,
            ["esi-calendar.read_calendar_events.v1"] = EsiScope.CalendarReadEvents,
            ["esi-calendar.respond_calendar_events.v1"] = EsiScope.CalendarRespondEvents,
            ["esi-characters.read_agents_research.v1"] = EsiScope.CharactersReadAgentsResearch,
            ["esi-characters.read_blueprints.v1"] = EsiScope.CharactersReadBlueprints,
            ["esi-characters.read_chat_channels.v1"] = EsiScope.CharactersReadChatChannels,
            ["esi-characters.read_contacts.v1"] = EsiScope.CharactersReadContacts,
            ["esi-characters.read_corporation_roles.v1"] = EsiScope.CharactersReadCorporationRoles,
            ["esi-characters.read_fatigue.v1"] = EsiScope.CharactersReadFatigue,
            ["esi-characters.read_loyalty.v1"] = EsiScope.CharactersReadLoyalty,
            ["esi-characters.read_medals.v1"] = EsiScope.CharactersReadMedals,
            ["esi-characters.read_opportunities.v1"] = EsiScope.CharactersReadOpportunities,
            ["esi-characters.read_standings.v1"] = EsiScope.CharactersReadStandings,
            ["esi-characters.write_contacts.v1"] = EsiScope.CharactersWriteContacts,
            ["esi-clones.read_clones.v1"] = EsiScope.ClonesRead,
            ["esi-clones.read_implants.v1"] = EsiScope.ClonesReadImplants,
            ["esi-contracts.read_character_contracts.v1"] = EsiScope.ContractsReadCharacter,
            ["esi-corporations.read_corporation_membership.v1"] = EsiScope.CorporationsReadCorporationMembership,
            ["esi-corporations.read_structures.v1"] = EsiScope.CorporationsReadStructures,
            ["esi-corporations.write_structures.v1"] = EsiScope.CorporationsWriteStructures,
            ["esi-fittings.read_fittings.v1"] = EsiScope.FittingsRead,
            ["esi-fittings.write_fittings.v1"] = EsiScope.FittingsWrite,
            ["esi-fleets.read_fleet.v1"] = EsiScope.FleetsReadFleet,
            ["esi-fleets.write_fleet.v1"] = EsiScope.FleetsWriteFleet,
            ["esi-industry.read_character_jobs.v1"] = EsiScope.IndustryReadCharacterJobs,
            ["esi-killmails.read_killmails.v1"] = EsiScope.KillmailsRead,
            ["esi-location.read_location.v1"] = EsiScope.LocationRead,
            ["esi-location.read_online.v1"] = EsiScope.LocationReadOnline,
            ["esi-location.read_ship_type.v1"] = EsiScope.LocationReadShipType,
            ["esi-mail.organize_mail.v1"] = EsiScope.MailOrganize,
            ["esi-mail.read_mail.v1"] = EsiScope.MailRead,
            ["esi-mail.send_mail.v1"] = EsiScope.MailSend,
            ["esi-markets.read_character_orders.v1"] = EsiScope.MarketsReadCharacterOrders,
            ["esi-markets.structure_markets.v1"] = EsiScope.MarketsStructure,
            ["esi-planets.manage_planets.v1"] = EsiScope.PlanetsManage,
            ["esi-search.search_structures.v1"] = EsiScope.SearchStructures,
            ["esi-skills.read_skillqueue.v1"] = EsiScope.SkillsReadSkillqueue,
            ["esi-skills.read_skills.v1"] = EsiScope.SkillsRead,
            ["esi-ui.open_window.v1"] = EsiScope.UiOpenWindow,
            ["esi-ui.write_waypoint.v1"] = EsiScope.UiWriteWaypoint,
            ["esi-universe.read_structures.v1"] = EsiScope.UniverseReadStructures,
            ["esi-wallet.read_character_wallet.v1"] = EsiScope.WalletReadCharacter,
            
		};

		public static IEnumerable<string> GetScopeStrings(EsiScope scope)
		{
			foreach (var kv in Strings)
			{
				if (scope.HasFlag(kv.Value))
					yield return kv.Key;
			}
		}

		public static EsiScope GetScopeEnum(params string[] scopes)
		{
			EsiScope scope = 0;
			foreach (var str in scopes)
			{
				scope |= Strings[str];
			}

			return scope;
		}
	}
}