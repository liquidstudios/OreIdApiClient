using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class LoginCodeVerifyResponse
    {
        [JsonProperty("processId")]
        public string processId { get; set; }
        [JsonProperty("success")]
        public bool success { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
    }
}