namespace UCG.siteTRAXLite.Entities
{
    public class UserInfoEntity
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool HasRegistered { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string Locale { get; set; }
        public DateTime PasswordExpiration { get; set; }
    }
}
