using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace EveESI
{
    public class TokenInfo
    {
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        public TokenInfo()
        {
            CreatedAt = DateTime.Now;
        }

        public bool IsExpired()
        {
            var expireTime = CreatedAt + TimeSpan.FromSeconds(ExpiresIn);
            return DateTime.Now > expireTime;
        }
    }
}
