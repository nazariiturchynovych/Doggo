// ReSharper disable CollectionNeverUpdated.Global
#pragma warning disable CS8618
namespace Doggo.Domain.Entities.Chat;

using Base;

public class Chat : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsPrivate { get; set; }

    public List<UserChat> UserChats { get; set; }

    public List<Message> Messages { get; set; }
}