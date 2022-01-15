using Newtonsoft.Json;

namespace OreIdApiClient.Models
{
    public class CustodialSignTransaction1
    {
        [JsonProperty("signed_transaction")]
        public string SignedTransaction { get; set; }

        [JsonProperty("process_id")]
        public string ProcessId { get; set; }
    }
}