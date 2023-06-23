using Newtonsoft.Json;

namespace UCG.siteTRAXLite.DataContracts.AuthenContracts
{
    public class AuthenRequestData
    {
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
