using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Evepraisal
{
    public class EvepraisalClient
    {
        private HttpClient _client;
        private string _boundary = "----WebKitFormBoundary674v0jrgFIdbv6SV";

        public EvepraisalClient(string baseUrl = "http://evepraisal.com/")
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
                DefaultRequestHeaders = { UserAgent = { new ProductInfoHeaderValue("OIBot", "1.0")}}
            };
        }

        public string GetAppraisalLink(string appraisalId)
        {
            return $"{_client.BaseAddress}a/{appraisalId}";
        }

        public async Task<Appraisal> AppraiseAsync(string rawText, Market market)
        {
            var rawTextData = new StringContent(rawText);
            rawTextData.Headers.ContentDisposition = ContentDispositionHeaderValue.Parse("form-data; name=\"raw_textarea\"");
            var marketData = new StringContent(market.ToString().ToLower());
            marketData.Headers.ContentDisposition = ContentDispositionHeaderValue.Parse("form-data; name=\"market\"");
            var content = new MultipartFormDataContent(_boundary)
            {
                rawTextData,
                marketData
            };
            var response = await _client.PostAsync("appraisal", content).ConfigureAwait(false);
            var appId = response.Headers.GetValues("x-appraisal-id").FirstOrDefault();
            var jsonResponse = await _client.GetStringAsync($"a/{appId}.json").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Appraisal>(jsonResponse);
        }
    }

    public enum Market
    {
        Jita,
        Amarr,
        Dodixie,
        Hek,
        Universe
    }

    public enum Kind
    {
        Eft,
        Listing,
        Assets
    }

    public class Appraisal
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonIgnore]
        public Kind Kind { get; set; }

        [JsonProperty("kind")]
        public string KindName
        {
            get => Kind.ToString().ToLower();
            set => Kind = (Kind)Enum.Parse(typeof(Kind), value, true);
        }

        [JsonIgnore]
        public Market Market { get; set; }

        [JsonProperty("market_name")]
        public string MarketName
        {
            get => Market.ToString().ToLower();
            set => Market = (Market)Enum.Parse(typeof(Market), value, true);
        }

        [JsonProperty("totals")]
        public Totals Totals { get; set; }

        [JsonProperty("items")]
        public Item[] Items { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }
    }

    public class Item
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("typeID")]
        public long TypeId { get; set; }

        [JsonProperty("typeName")]
        public string TypeName { get; set; }

        [JsonProperty("typeVolume")]
        public float TypeVolume { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("prices")]
        public Prices Prices { get; set; }
    }

    public class Totals
    {
        [JsonProperty("buy")]
        public float Buy { get; set; }

        [JsonProperty("sell")]
        public float Sell { get; set; }

        [JsonProperty("volume")]
        public float Volume { get; set; }
    }

    public class Prices
    {
        [JsonProperty("all")]
        public Price All { get; set; }

        [JsonProperty("buy")]
        public Price Buy { get; set; }

        [JsonProperty("sell")]
        public Price Sell { get; set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        [JsonProperty("strategy")]
        public string Strategy { get; set; }
    }

    public class Price
    {
        [JsonProperty("avg")]
        public float Average { get; set; }

        [JsonProperty("max")]
        public float Max { get; set; }

        [JsonProperty("median")]
        public float Median { get; set; }

        [JsonProperty("min")]
        public float Min { get; set; }

        [JsonProperty("percentile")]
        public long Percentile { get; set; }

        [JsonProperty("stddev")]
        public float StdDev { get; set; }

        [JsonProperty("volume")]
        public long Volume { get; set; }

        [JsonProperty("order_count")]
        public long OrderCount { get; set; }
    }
}
