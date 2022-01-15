using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class TokenAirdropRequest
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("chain_account")]
        public string ChainAccount { get; set; }

        [JsonProperty("chain_network")]
        public string ChainNetwork { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}