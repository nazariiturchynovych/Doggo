namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserChatConfiguration : IEntityTypeConfiguration<UserChat>
{
    public void Configure(EntityTypeBuilder<UserChat> builder)
    {
        builder.ToTable("user_chats");

        builder.HasKey(x => new { x.UserId, x.ChatId });

    }
}