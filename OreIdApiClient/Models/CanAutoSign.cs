using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class CanAutoSign
    {
        [JsonProperty("autoSignCredentialsExist")]
        public bool AutoSignCredentialsExist { get; set; }

        [JsonProperty("canCreateAutoSignCredentials")]
        public bool CanCreateAutoSignCredentials { get; set; }

        [JsonProperty("maxAutoSignValidForInSeconds")]
        public int MaxAutoSignValidForInSeconds { get; set; }
    }
}