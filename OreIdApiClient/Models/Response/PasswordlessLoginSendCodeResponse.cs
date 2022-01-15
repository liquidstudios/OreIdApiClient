using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class PasswordlessLoginSendCodeResponse
    {
        [JsonProperty("processId")]
        public string processId { get; set; }
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}