using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class Permission
    {
        [JsonProperty("chainNetwork")]
        public string ChainNetwork { get; set; }
        [JsonProperty("chainAccount")]
        public string ChainAccount { get; set; }
        [JsonProperty("publicKey")]
        public string PublicKey { get; set; }
        [JsonProperty("privateKeyStoredExterally")]
        public bool PrivateKeyStoredExterally { get; set; }
        [JsonProperty("externalWalletType")]
        public string ExternalWalletType { get; set; }
        [JsonProperty("accountType")]
        public string AccountType { get; set; }
        [JsonProperty("isVerified")]
        public bool IsVerified { get; set; }
        [JsonProperty("permission")]
        public string permission { get; set; }
    }
}