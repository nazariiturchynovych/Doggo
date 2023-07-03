namespace Doggo.Infrastructure.Persistence.EntityConfiguration;

using Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ChatConfigurations : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("chats");

        builder.HasMany(x => x.Messages)
            .WithOne(x => x.Chat)
            .HasForeignKey(x => x.ChatId);

        builder.HasMany(x => x.UserChats)
            .WithOne(x => x.Chat)
            .HasForeignKey(x => x.ChatId);
    }
}
