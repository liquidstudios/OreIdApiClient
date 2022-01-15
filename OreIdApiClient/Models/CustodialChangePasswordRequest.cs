using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class CustodialChangePasswordRequest
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("current_password")]
        public string CurrentPassword { get; set; }

        [JsonProperty("new_password")]
        public string NewPassword { get; set; }
    }
}