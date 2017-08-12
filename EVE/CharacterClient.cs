using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveESI
{
    public class CharacterClient
    {
        private EsiClient _client;

        internal CharacterClient(EsiClient client)
        {
            _client = client;
        }

        public Task<CharacterInfo> Get(long characterId)
        {
            return _client.GetAsync<CharacterInfo>($"characters/{characterId}");
        }

        public Task<IconInfo> GetPortrait(long characterId)
        {
            return _client.GetAsync<IconInfo>($"characters/{characterId}/portrait");
        }
    }
}
