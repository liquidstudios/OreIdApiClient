using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class AppTokenRequest
    {
        [JsonProperty("newAccountPassword", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string NewAccountPassword { get; set; }
        [JsonProperty("currentAccountPassword", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string CurrentAccountPassword { get; set; }

        // Todo: AppTokenRequest - Fix this depending on the flow given
        //[JsonProperty("secrets", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        //public string Secrets { get; set; }
    }
}