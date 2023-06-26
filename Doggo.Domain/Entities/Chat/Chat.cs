namespace Doggo.Domain.Entities.Chat;

using Base;

public class Chat : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsOneToOne { get; set; }

    public List<UserChat> UserChats { get; set; }

    public List<Message> Messages { get; set; }
}

public class ChatDto
{
    public Guid ChatId { get; set; }

    public List<string> ConnectionIds { get; set; }
}