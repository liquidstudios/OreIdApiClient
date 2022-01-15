using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class TokenAirdrop
    {
        [JsonProperty("processId")]
        public string ProcessId { get; set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }
    }
}