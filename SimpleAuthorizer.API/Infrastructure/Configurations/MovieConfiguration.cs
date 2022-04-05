using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleAuthorizer.API.Domain.Models;

namespace SimpleAuthorizer.API.Infrastructure.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.ReleaseDate).IsRequired();
        }
    }
}
