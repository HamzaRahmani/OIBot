using System;
using System.Collections.Generic;
using System.Text;

namespace EveESI
{
    [Flags]
    public enum EntityType
    {
        Agent = 1<<0,
        Alliance = 1<<1,
        Character = 1<<2,
        Constellation = 1<<3,
        Corporation = 1<<4,
        Faction = 1<<5,
        InventoryType = 1<<6,
        Region = 1<<7,
        SolarSystem = 1<<8,
        Station = 1<<9,
        Wormhole = 1<<10
    }
}
