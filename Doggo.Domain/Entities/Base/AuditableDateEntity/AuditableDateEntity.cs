namespace Doggo.Domain.Entities.Base.AuditableDateEntity;


public abstract class AuditableDateEntity : IAuditableDateEntity
{
    protected AuditableDateEntity() => CreatedDate = DateTime.UtcNow;
    public DateTime CreatedDate { get; set; }

    public DateTime? ChangedDate { get; set; }

}