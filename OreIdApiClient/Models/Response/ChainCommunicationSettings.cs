using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class ChainCommunicationSettings
    {
        [JsonProperty("blocksToCheck")]
        public int BlocksToCheck { get; set; }
        [JsonProperty("checkIntervalInMs")]
        public int CheckIntervalInMs { get; set; }
        [JsonProperty("getBlockAttempts")]
        public int GetBlockAttempts { get; set; }
    }
}