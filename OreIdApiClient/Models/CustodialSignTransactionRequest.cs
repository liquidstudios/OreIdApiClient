using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class CustodialSignTransactionRequest
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("broadcast")]
        public bool Broadcast { get; set; }

        [JsonProperty("chain_account")]
        public string ChainAccount { get; set; }

        [JsonProperty("chain_network")]
        public string ChainNetwork { get; set; }

        [JsonProperty("transaction")]
        public string Transaction { get; set; }

        [JsonProperty("user_password")]
        public string UserPassword { get; set; }
    }
}