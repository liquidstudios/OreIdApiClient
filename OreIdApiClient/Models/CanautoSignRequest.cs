using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class CanautoSignRequest
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("chain_network")]
        public string ChainNetwork { get; set; }

        [JsonProperty("chain_account")]
        public string ChainAccount { get; set; }

        [JsonProperty("transaction")]
        public string Transaction { get; set; }
    }
}