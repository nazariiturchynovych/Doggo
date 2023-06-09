namespace Doggo.Domain.Entities.Base.AuditableDateEntity;

public abstract class AuditableDateEntity : Entity, IAuditableDateEntity
{
    protected AuditableDateEntity() => CreatedDate = DateTime.UtcNow;
    public DateTime CreatedDate { get; set; }

    public DateTime? ChangedDate { get; set; }
}