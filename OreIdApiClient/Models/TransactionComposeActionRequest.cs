using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class TransactionComposeActionRequest
    {
        [JsonProperty("chain_network")]
        public string ChainNetwork { get; set; }
        [JsonProperty("chain_action_type")]
        public string ChainActionType { get; set; }
        [JsonProperty("action_params")]
        public string ActionParams { get; set; }
    }
}