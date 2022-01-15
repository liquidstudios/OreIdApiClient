using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class GetChainAccountRequest
    {
        [JsonProperty("account_name")]
        public string AccountName { get; set; }
    }
}