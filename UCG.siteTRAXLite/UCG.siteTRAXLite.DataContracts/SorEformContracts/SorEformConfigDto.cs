using Newtonsoft.Json;

namespace UCG.siteTRAXLite.DataContracts.SorEformContracts
{
    public class SorEformConfigDTO
    {
        [JsonProperty("configInfo")]
        public ConfigInfoDTO ConfigInfo { get; set; }
        [JsonProperty("jobTab")]
        public JobTabDTO JobTab { get; set; }
    }

    public class ConfigInfoDTO
    {
        [JsonProperty("configVersion")]
        public int ConfigVersion { get; set; }
    }

    public class JobTabDTO
    {
        [JsonProperty("sections")]
        public List<SectionDTO> Sections { get; set; }
    }

    public class SectionDTO
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("sectionType")]
        public string SectionType { get; set; }
        [JsonProperty("breadcrumbs")]
        public List<BreadcrumbDTO> Breadcrumbs { get; set; }
    }

    public class BreadcrumbDTO
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("actionList")]
        public List<ActionItemDTO> ActionList { get; set; }
    }

    public class ActionItemDTO
        {
        [JsonProperty("condition")]
        public ConditionDTO Condition { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("responseType")]
        public string ResponseType { get; set; }
        [JsonProperty("logic")]
        public string Logic { get; set; }
        [JsonProperty("responseData")]
        public List<string> ResponseData { get; set; }
        [JsonProperty("actionList")]
        public List<ActionItemDTO> SubActionList { get; set; }
    }

    public class ConditionDTO
    {
        [JsonProperty("responseData")]
        public string ResponseData { get; set; }
    }
}
