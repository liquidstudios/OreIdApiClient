using Newtonsoft.Json;
using System.Collections.Generic;

namespace OreIdApiClient.Models
{
    public class Chain
    {
        [JsonProperty("logoUrl")]
        public string LogoUrl { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("network")]
        public string Network { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("isTestNetwork")]
        public bool IsTestNetwork { get; set; }
        [JsonProperty("blockExplorerAccountUrl")]
        public string BlockExplorerAccountUrl { get; set; }
        [JsonProperty("blockExplorerTxUrl")]
        public string BlockExplorerTxUrl { get; set; }
        [JsonProperty("hosts")]
        public List<Host> Hosts { get; set; }
        [JsonProperty("chainCommunicationSettings")]
        public ChainCommunicationSettings ChainCommunicationSettings { get; set; }
        [JsonProperty("defaultTransactionSettings")]
        public DefaultTransactionSettings DefaultTransactionSettings { get; set; }
        [JsonProperty("createBridgeContract")]
        public string CreateBridgeContract { get; set; }
        [JsonProperty("dfuseNetwork")]
        public string DfuseNetwork { get; set; }
        [JsonProperty("monitorConfig")]
        public MonitorConfig MonitorConfig { get; set; }
        [JsonProperty("dFuseSupported")]
        public bool? DFuseSupported { get; set; }
    }
}