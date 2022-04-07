using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation.AspNetCore;
using SimpleAuthorizer.Common.Mappings;

namespace SimpleAuthorizer.Common
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase<TDbContext>(
            this IServiceCollection services,
            IConfiguration configuration)
            where TDbContext : DbContext
            => services
                .AddScoped<DbContext, TDbContext>()
                .AddDbContext<TDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetDefaultConnectionString()));

        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            Assembly assembly)
        {
            services.AddAutoMapper(
                    (_, config) => config
                        .AddProfile(new MappingProfile(assembly)),
                    Array.Empty<Assembly>());

            services.AddFluentValidation(validation => validation
                    .RegisterValidatorsFromAssembly(assembly));
            services.AddMediatR(assembly);

            return services;
        }

        private static IServiceCollection AddIdentity(
             this IServiceCollection services,
             IConfiguration configuration)
        {
            services.AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = "bearer";
                    options.DefaultChallengeScheme = "bearer";
                })
                .AddJwtBearer("bearer", options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtToken:Key"))),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero //the default for this setting is 5 minutes
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context => {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Access-Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}
