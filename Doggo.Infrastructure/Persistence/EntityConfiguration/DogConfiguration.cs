namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.DogOwner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DogConfiguration : IEntityTypeConfiguration<Dog>
{
    public void Configure(EntityTypeBuilder<Dog> builder)
    {
        builder.ToTable("Dogs");

        builder.HasMany(x => x.JobRequest)
            .WithOne(x => x.Dog)
            .HasForeignKey(x => x.DogId);
    }
}