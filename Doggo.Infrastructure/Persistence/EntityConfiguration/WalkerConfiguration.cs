namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.Walker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class WalkerConfiguration : IEntityTypeConfiguration<Walker>
{
    public void Configure(EntityTypeBuilder<Walker> builder)
    {
        builder.ToTable("walkers");

        builder.HasMany(w => w.Jobs)
            .WithOne(j => j.Walker)
            .HasForeignKey(j => j.WalkerId);

        builder.HasMany(x => x.PossibleSchedules)
            .WithOne(x => x.Walker)
            .HasForeignKey(x => x.WalkerId);
    }
}