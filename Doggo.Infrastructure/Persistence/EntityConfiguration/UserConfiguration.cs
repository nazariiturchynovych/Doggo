namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.DogOwner;
using Domain.Entities.User;
using Domain.Entities.User.Documents;
using Domain.Entities.Walker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.NormalizedEmail);

        builder.HasOne(x => x.PersonalIdentifier)
            .WithOne(x => x.User)
            .HasForeignKey<PersonalIdentifier>(x => x.UserId);

        builder.HasOne(x => x.Walker)
            .WithOne(x => x.User)
            .HasForeignKey<Walker>(x => x.UserId);

        builder.HasOne(x => x.DogOwner)
            .WithOne(x => x.User)
            .HasForeignKey<DogOwner>(x => x.UserId);

        builder.HasMany(x => x.UserRoles)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .IsRequired();

        builder.HasMany(x => x.UserChats)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Messages)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.RefreshToken)
            .WithOne(x => x.User)
            .HasForeignKey<RefreshToken>(x => x.UserId);
    }
}