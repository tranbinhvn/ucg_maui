using Newtonsoft.Json;

namespace UCG.siteTRAXLite.DataContracts
{
    public class LaunchDataDTO
    {
        [JsonProperty("CRN")]
        public string CRN { get; set; }
        [JsonProperty("SiteName")]
        public string SiteName { get; set; }
    }
}
