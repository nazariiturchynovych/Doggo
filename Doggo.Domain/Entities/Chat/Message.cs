namespace Doggo.Domain.Entities.Chat;

using User;

public class Message
{
    public string Value { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public Guid ChatId { get; set; }

    public Chat Chat { get; set; }

}