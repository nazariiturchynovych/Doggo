namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserLoginConfigurations : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.ToTable("UserLogins");

        builder.HasKey(
            ul => new
            {
                ul.LoginProvider,
                ul.ProviderKey
            });
    }
}