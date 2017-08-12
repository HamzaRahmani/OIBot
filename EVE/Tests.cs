using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EveESI
{
    public class Tests
    {
        [Fact]
        public void ScopeMapTest()
        {
            var scopes = EsiScopeMap.GetScopeStrings(EsiScope.CharactersReadMedals | EsiScope.AllClones).ToList();

            Assert.True(scopes.Contains("esi-characters.read_medals.v1"));
            Assert.True(scopes.Contains("esi-clones.read_clones.v1"));
            Assert.True(scopes.Contains("esi-clones.read_implants.v1"));
        }
    }
}
