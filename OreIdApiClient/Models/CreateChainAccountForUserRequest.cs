using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class CreateChainAccountForUserRequest
    {
        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("account_type")]
        public string AccountType { get; set; }
        [JsonProperty("chain_network")]
        public string ChainNetwork { get; set; }
        [JsonProperty("user_password")]
        public string UserPassword { get; set; }
    }
}