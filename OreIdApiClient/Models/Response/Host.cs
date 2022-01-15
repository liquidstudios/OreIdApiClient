using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class Host
    {
        [JsonProperty("host")]
        public string host { get; set; }
        [JsonProperty("protocol")]
        public string Protocol { get; set; }
        [JsonProperty("chainId")]
        public string ChainId { get; set; }
        [JsonProperty("port")]
        public object Port { get; set; }
        [JsonProperty("forkName")]
        public string ForkName { get; set; }
    }
}