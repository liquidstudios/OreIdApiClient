using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class ChainsConfiguration
    {
        [JsonProperty("processId")]
        public string ProcessId { get; set; }
        [JsonProperty("values")]
        public Values Values { get; set; }

    }
}