using Newtonsoft.Json;

namespace UCG.siteTRAXLite.DataContracts.ModelStateContracts
{
    public class ModelState
    {
        [JsonProperty("$id")]
        public int Id { get; set; }
        [JsonProperty("Error")]
        public List<string> Error { get; set; }
    }
}
