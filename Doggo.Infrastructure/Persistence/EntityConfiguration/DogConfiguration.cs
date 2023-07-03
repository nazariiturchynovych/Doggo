namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.Dog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DogConfiguration : IEntityTypeConfiguration<Dog>
{
    public void Configure(EntityTypeBuilder<Dog> builder)
    {
        builder.ToTable("dogs");

        builder.HasMany(x => x.JobRequests)
            .WithOne(x => x.Dog)
            .HasForeignKey(x => x.DogId);
    }
}