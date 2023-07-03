#pragma warning disable CS8618
namespace Doggo.Domain.Entities.Base.JobRequestEntity;

using JobRequest;

public abstract class JobRequestOwnedEntity : Entity, IJobRequestOwnedEntity
{
    public Guid JobRequestId { get; set; }

    public JobRequest JobRequest { get; set; }
}