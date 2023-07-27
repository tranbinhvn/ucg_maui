using Newtonsoft.Json;
using UCG.siteTRAXLite.Entities;

namespace UCG.siteTRAXLite.DataContracts
{
    public class LaunchDataDTO
    {
        [JsonProperty("JobDetail")]
        public JobDetailDTO JobDetail { get; set; }
    }
}
