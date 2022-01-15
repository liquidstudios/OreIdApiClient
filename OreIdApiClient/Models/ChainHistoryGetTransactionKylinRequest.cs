using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class ChainHistoryGetTransactionKylinRequest
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}