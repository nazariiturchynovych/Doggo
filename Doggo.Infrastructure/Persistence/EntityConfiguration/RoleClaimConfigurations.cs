namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RoleClaimConfigurations : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable("role_claims");

        builder.HasKey(x => x.Id);
    }
}