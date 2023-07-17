using Newtonsoft.Json;

namespace UCG.siteTRAXLite.DataContracts.SorEformContracts
{
    public class SorEformConfigDto
    {
        [JsonProperty("configInfo")]
        public ConfigInfoDto ConfigInfo { get; set; }
        [JsonProperty("settings")]
        public SettingsDto Settings { get; set; }
    }

    public class ConfigInfoDto
    {
        [JsonProperty("configVersion")]
        public int ConfigVersion { get; set; }
    }

    public class SettingsDto
    {
        [JsonProperty("outcomeOptions")]
        public List<OutcomeOptionDto> OutcomeOptions { get; set; }
    }

    public class OutcomeOptionDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("actionList")]
        public List<ActionItemDto> ActionList { get; set; }
    }

    public class ActionItemDto
    {
        [JsonProperty("condition")]
        public ConditionDto Condition { get; set; }
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
        public List<ActionItemDto> SubActionList { get; set; }
    }

    public class ConditionDto
    {
        [JsonProperty("responseData")]
        public string ResponseData { get; set; }
    }
}
