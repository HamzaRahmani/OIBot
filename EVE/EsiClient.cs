using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EveESI
{
    public class EsiClient
    {
        private string _clientId;
        private string _clientAuth;
        private HttpClient _client;
        private TokenInfo _token;

        public string BaseUrl { get; } = "https://esi.tech.ccp.is/latest/";

        public AllianceClient Alliances { get; }
        public CorporationClient Corporations { get; }
        public CharacterClient Characters { get; }
        public SearchClient Search { get; }

        private Dictionary<string, CachedData> _requestCache { get; } = new Dictionary<string, CachedData>();

        public EsiClient(string clientId, string clientSecret, ProductInfoHeaderValue userAgent)
        {
            _clientId = clientId;
            var auth = $"{clientId}:{clientSecret}";
            _clientAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes(auth));
            Console.WriteLine(_clientAuth);
            _client = new HttpClient
            {
                DefaultRequestHeaders =
                {
                    UserAgent = { userAgent },
                }
            };

            Alliances = new AllianceClient(this);
            Corporations = new CorporationClient(this);
            Characters = new CharacterClient(this);
            Search = new SearchClient(this);
        }

        public bool GetCachedData<T>(string request, out T data)
        {
            if (_requestCache.ContainsKey(request))
            {
                var cache = _requestCache[request];
                if (cache.IsExpired())
                {
                    _requestCache.Remove(request);
                    data = default(T);
                    return false;
                }
                else
                {
                    data = (T)cache.Data;
                    return true;
                }
            }

            data = default(T);
            return false;
        }

        public void CacheData(string request, object data, DateTime expireTime)
        {
            var cache = new CachedData(expireTime, data);
            if (_requestCache.ContainsKey(request))
            {
                _requestCache[request] = cache;
            }
            else
            {
                _requestCache.Add(request, cache);
            }
        }

        public async Task<TokenInfo> TrySetTokenAsync(TokenInfo token)
        {
            if (token.IsExpired())
            {
                token = await RefreshTokenAsync(token.RefreshToken).ConfigureAwait(false);
            }

            var info = await VerifyToken(token.AccessToken).ConfigureAwait(false);
            if (info != null)
            {
                _token = token;
                return token;
            }

            return null;
        }

        public string GetAuthUrl(string callbackUri, EsiScope scope, string state)
        {
            var escScopes = Uri.EscapeDataString(string.Join(" ", EsiScopeMap.GetScopeStrings(scope)));
            var escCallback = Uri.EscapeDataString(callbackUri);
            var escState = Uri.EscapeDataString(state);
            return $"https://login.eveonline.com/oauth/authorize?response_type=code&redirect_uri={escCallback}&realm=ESI&client_id={_clientId}&scope={escScopes}&state={escState}";
        }

        public async Task<T> GetAsync<T>(string requestUri)
        {
            if (GetCachedData(requestUri, out T data))
                return data;

            var fullUri = $"{BaseUrl}{requestUri}";
            var response = await _client.GetAsync(fullUri).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException($"{(int)response.StatusCode}: {response.Content.ReadAsStringAsync().Result}");

            var expireTime = DateTime.Parse(response.Content.Headers.GetValues("expires").FirstOrDefault()); 
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var obj = JsonConvert.DeserializeObject<T>(responseContent);
            Console.WriteLine($"{DateTime.Now}\n  Request: {fullUri}\n  Expires: {expireTime}");
            CacheData(requestUri, obj, expireTime);
            return obj;
        }

        public async Task<TokenInfo> AuthorizeAsync(string authCode)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _clientAuth);
            var content = new StringContent($"grant_type=authorization_code&code={authCode}", Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await _client.PostAsync("https://login.eveonline.com/oauth/token", content).ConfigureAwait(false);
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {(int)response.StatusCode} {response.ReasonPhrase}");
                Console.WriteLine(responseContent);
                Console.WriteLine("---");
                return null;
            }
            _client.DefaultRequestHeaders.Authorization = null;
            return JsonConvert.DeserializeObject<TokenInfo>(responseContent);
        }

        public async Task<TokenInfo> RefreshTokenAsync(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _clientAuth);
            var requestContent = new StringContent($"grant_type=refresh_token&refresh_token={token}", Encoding.UTF8, "applications/x-www-form-urlencoded");
            var response = await _client.PostAsync("https://login.eveonline.com/oauth/token", requestContent).ConfigureAwait(false);
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {(int)response.StatusCode} {response.ReasonPhrase}");
                Console.WriteLine(responseContent);
                Console.WriteLine("---");
                return null;
            }
            _client.DefaultRequestHeaders.Authorization = null;
            return JsonConvert.DeserializeObject<TokenInfo>(responseContent);
        }

        public async Task<VerificationInfo> VerifyToken(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("https://login.eveonline.com/oauth/verify").ConfigureAwait(false);
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {(int)response.StatusCode} {response.ReasonPhrase}");
                Console.WriteLine(responseContent);
                Console.WriteLine("---");
                return null;
            }
            _client.DefaultRequestHeaders.Authorization = null;
            return JsonConvert.DeserializeObject<VerificationInfo>(responseContent);
        }
    }

    //TODO: merge with character info?
    public class VerificationInfo
    {
        [JsonProperty("CharacterID")]
        public long CharacterId { get; set; }

        [JsonProperty("CharacterName")]
        public string CharacterName { get; set; }

        public DateTime ExpiresOn { get; set; }

        [JsonProperty("Scopes")]
        public string ScopesString
        {
            get => string.Join(" ", EsiScopeMap.GetScopeStrings(Scopes));
            set => Scopes = EsiScopeMap.GetScopeEnum(value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
        }

        [JsonIgnore]
        public EsiScope Scopes { get; set; }

        [JsonProperty("TokenType")]
        public string TokenType { get; set; }

        [JsonProperty("CharacterOwnerHash")]
        public string CharacterOwnerHash { get; set; }
    }

    public class CachedData
    {
        public DateTime Expiration { get; }
        public object Data { get; }

        public CachedData(DateTime expiration, object data)
        {
            Expiration = expiration;
            Data = data;
        }

        public bool IsExpired()
        {
            return Expiration < DateTime.Now;
        }
    }
}
