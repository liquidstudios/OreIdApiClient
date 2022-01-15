using Newtonsoft.Json;
using System.Collections.Generic;

namespace OreIdApiClient.Models
{
    public class UserInfo
    {
        [JsonProperty("processId")]
        public string ProcessId { get; set; }
        [JsonProperty("accountName")]
        public string AccountName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("picture")]
        public string Picture { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("permissions")]
        public List<Permission> Permissions { get; set; }
    }
}