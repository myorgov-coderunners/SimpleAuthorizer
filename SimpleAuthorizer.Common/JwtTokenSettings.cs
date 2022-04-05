
namespace SimpleAuthorizer.Common
{
    public class JwtTokenSettings
    {
        public string Key { get; set; } = default!;
        public int ExpiryMinutes { get; set; }
        public string Audience { get; set; } = default!;
        public string Issuer { get; set; } = default!;
    }
}
