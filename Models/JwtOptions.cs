namespace NetCommApi.Models
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecureKey { get; set; }
    }
}
