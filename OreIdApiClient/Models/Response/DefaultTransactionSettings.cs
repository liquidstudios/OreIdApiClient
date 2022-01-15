using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class DefaultTransactionSettings
    {
        [JsonProperty("blocksBehind")]
        public int BlocksBehind { get; set; }
        [JsonProperty("expireSeconds")]
        public int ExpireSeconds { get; set; }
    }
}