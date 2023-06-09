namespace Doggo.Domain.Entities.Base.AuditableDateEntity;

public interface IAuditableDateEntity
{
    public DateTime CreatedDate { get; set; }

    public DateTime? ChangedDate { get; set; }
}