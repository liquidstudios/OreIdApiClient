using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class MonitorConfig
    {
        [JsonProperty("dfuseSupported")]
        public bool DfuseSupported { get; set; }
        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }
    }
}