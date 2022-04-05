using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace SimpleAuthorizer.Common
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder Initialize(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            var db = serviceProvider.GetRequiredService<DbContext>();

            db.Database.Migrate();

            //var seeders = serviceProvider.GetServices<IDataSeeder>();

            //foreach (var seeder in seeders)
            //{
            //    seeder.SeedData();
            //}
            return app;
        }
    }
}
