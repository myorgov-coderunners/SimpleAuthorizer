using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleAuthorizer.Identity.Infrastructure.Models;
using System.Reflection;

namespace SimpleAuthorizer.Identity.Infrastructure
{
    public class SimpleIdentityDbContext : IdentityDbContext<User>
    {
        public SimpleIdentityDbContext(DbContextOptions<SimpleIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
