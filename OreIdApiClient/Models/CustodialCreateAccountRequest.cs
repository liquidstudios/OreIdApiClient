using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class CustodialCreateAccountRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("user_password")]
        public string UserPassword { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("account_type")]
        public string AccountType { get; set; }
    }
}