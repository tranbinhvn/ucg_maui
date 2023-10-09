using Newtonsoft.Json;

namespace UCG.siteTRAXLite.DataContracts.AuthenContracts
{
    public class AuthenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty(".issued")]
        public DateTime Issued { get; set; }
        [JsonProperty(".expires")]
        public DateTime Expires { get; set; }
        [JsonProperty("refresh_token")]
        public string ResfreshToken { get; set; }
    }
}
