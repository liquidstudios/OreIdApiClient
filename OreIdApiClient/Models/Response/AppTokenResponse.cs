using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class AppTokenResponse
    {
        [JsonProperty("processId")]
        public string ProcessId { get; set; }
        [JsonProperty("appAccessToken")]
        public string AppAccessToken { get; set; }
    }
}