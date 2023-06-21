namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.DogOwner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DogOwnerConfiguration : IEntityTypeConfiguration<DogOwner>
{
    public void Configure(EntityTypeBuilder<DogOwner> builder)
    {
        builder.ToTable("DogOwners");

        builder.HasIndex(x => x.District);

        builder.HasMany(x => x.Dogs)
            .WithOne(x => x.DogOwner)
            .HasForeignKey(x => x.DogOwnerId);

        builder.HasMany(x => x.Jobs)
            .WithOne(x => x.DogOwner)
            .HasForeignKey(j => j.DogOwnerId);

        builder.HasMany(x => x.JobRequests)
            .WithOne(x => x.DogOwner)
            .HasForeignKey(x => x.DogOwnerId);
    }
}