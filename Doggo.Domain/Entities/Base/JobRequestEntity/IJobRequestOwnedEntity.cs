namespace Doggo.Domain.Entities.Base.JobRequest;

using Doggo.Domain.Entities.Base;
using Entities.JobRequest;

public interface IJobRequestOwnedEntity
{
    public int JobRequestId { get; set; }

    public JobRequest JobRequest { get; set; }
}