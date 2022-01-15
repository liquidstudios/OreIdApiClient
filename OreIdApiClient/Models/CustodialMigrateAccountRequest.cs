using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class CustodialMigrateAccountRequest
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("chain_account")]
        public string ChainAccount { get; set; }

        [JsonProperty("chain_network")]
        public string ChainNetwork { get; set; }

        [JsonProperty("user_password")]
        public string UserPassword { get; set; }

        [JsonProperty("to_type")]
        public string ToType { get; set; }
    }
}