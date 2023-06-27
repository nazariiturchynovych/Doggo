#pragma warning disable CS8618
namespace Doggo.Domain.Entities.Chat;

using Base;
using Base.AuditableDateEntity;
using User;

public class Message : IEntity, IAuditableDateEntity
{
    public Guid Id { get; set; }

    public string Value { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ChangedDate { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public Guid ChatId { get; set; }

    public Chat Chat { get; set; }
}