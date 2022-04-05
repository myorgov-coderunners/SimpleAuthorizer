using Microsoft.EntityFrameworkCore;
using SimpleAuthorizer.API.Domain.Models;
using System.Reflection;

namespace SimpleAuthorizer.API.Infrastructure
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Director> Directors { get; set; } = default!;

        public DbSet<Movie> Movies { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
