using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class ChainHistoryGetTransactionEosMainRequest
    {
        [JsonProperty("account_name")]
        public string AccountName { get; set; }
    }
}