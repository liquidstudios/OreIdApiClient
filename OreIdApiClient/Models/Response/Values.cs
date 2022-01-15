using Newtonsoft.Json;
using System.Collections.Generic;

namespace OreIdApiClient.Models
{
    public class Values
    {
        [JsonProperty("chains")]
        public List<Chain> Chains { get; set; }
    }
}