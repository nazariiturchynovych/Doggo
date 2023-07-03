namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserTokenConfigurations : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable("user_tokens");

        builder.HasKey(
            ut => new
            {
                ut.Name,
                ut.LoginProvider
            });
    }
}