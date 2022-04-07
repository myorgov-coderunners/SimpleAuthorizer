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
using static SimpleAuthorizer.Common.IdentityConstants;

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

        public static IServiceCollection AddJWTAuthentication(
             this IServiceCollection services,
             IConfiguration configuration)
        {
            var key = Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtTokenSettings:Key"));
            services
                .AddHttpContextAccessor()
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    bearer.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Access-Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                foreach (var constant in typeof(CustomClaims).GetFields())
                {
                    var value = constant.GetValue(null) ?? "";
                    options.AddPolicy(value.ToString()!, policy =>
                    {
                        policy.RequireClaim("Permission", value.ToString()!);
                        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    });
                }
            });

            return services;
        }
    }
}
