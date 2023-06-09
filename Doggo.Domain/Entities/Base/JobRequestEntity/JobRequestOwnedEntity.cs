namespace Doggo.Domain.Entities.Base.JobRequest;

using Doggo.Domain.Entities.Base;
using Entities.JobRequest;

public abstract class JobRequestOwnedEntity : Entity, IJobRequestOwnedEntity
{
    public int JobRequestId { get; set; }

    public JobRequest JobRequest { get; set; }
}