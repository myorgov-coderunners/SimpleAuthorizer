using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleAuthorizer.API.Domain.Models;

namespace SimpleAuthorizer.API.Infrastructure.Configurations
{
    public class DirectorConfiguration : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.BirthDate).IsRequired();
            builder.Property(p => p.Name).IsRequired();

            builder
                .HasMany(x => x.Movies)
                .WithOne(x => x.Director)
                .HasForeignKey("DirectorId")
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata
                .PrincipalToDependent
                !.SetField("_movies");
        }
    }
}
