using Microsoft.Extensions.Configuration;

namespace SimpleAuthorizer.Common
{
    public static class ConfigurationExtensions
    {
        public static string GetDefaultConnectionString(this IConfiguration configuration)
            => configuration.GetConnectionString("DefaultConnection");
    }
}
