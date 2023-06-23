using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCG.siteTRAXLite.DataContracts.ModelStateContracts
{
    public class ModelMessageResponse
    {
        [JsonProperty("$id")]
        public int Id { get; set; }
        [JsonProperty("ModelState")]
        public ModelState ModelState { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }

    public class ExceptionMessageResponse
    {
        [JsonProperty("$id")]
        public int Id { get; set; }
        [JsonProperty("Message")]
        public string Message { get; set; }
        [JsonProperty("ExceptionMessage")]
        public string ExceptionMessage { get; set; }
    }

    public class ModelUnleashMessageResponse
    {
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("DebugInformation")]
        public string DebugInformation { get; set; }
    }
}
