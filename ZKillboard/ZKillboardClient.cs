using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZKillboard
{
    public class ZKillboardClient : IDisposable
    {
        private readonly string _baseUrl;
        private readonly Uri _apiAddress;
        private readonly HttpClient _httpClient;

        public ZKillboardClient(string baseUrl = "https://zkillboard.com/")
        {
            _baseUrl = baseUrl;
            _apiAddress = new Uri(baseUrl + "api/");
            _httpClient = new HttpClient
            {
                BaseAddress = _apiAddress,
                DefaultRequestHeaders =
                {
                    AcceptEncoding = { new StringWithQualityHeaderValue("gzip") },
                    UserAgent = { new ProductInfoHeaderValue(new ProductHeaderValue("OIBot"))}
                }
            };
        }

        public string GetCharacterLink(long characterId)
        {
            return $"{_baseUrl}character/{characterId}/";
        }

        public string GetCorpLink(long corpId)
        {
            return $"{_baseUrl}corporation/{corpId}/";
        }

        public string GetAllianceLink(long allianceId)
        {
            return $"{_baseUrl}alliance/{allianceId}/";
        }

        public async Task<KillMail[]> GetKillMailsAsync(KillMailRequest request)
        {
            var requestString = _apiAddress + request.GetRequestString();
            var response = await _httpClient.GetStringAsync(requestString).ConfigureAwait(false);
            var killMails = JsonConvert.DeserializeObject<KillMail[]>(response);
            Console.WriteLine(killMails.Length);
            return killMails;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
