using Newtonsoft.Json;

namespace UCG.siteTRAXLite.DataContracts.UserInfoContracts
{
    public class UserInfoDto
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("HasRegistered")]
        public bool HasRegistered { get; set; }
        [JsonProperty("LastLoginDate")]
        public DateTime LastLoginDate { get; set; }
        [JsonProperty("Locale")]
        public string Locale { get; set; }
        [JsonProperty("PasswordExpiration")]
        public DateTime PasswordExpiration { get; set; }
    }
}
