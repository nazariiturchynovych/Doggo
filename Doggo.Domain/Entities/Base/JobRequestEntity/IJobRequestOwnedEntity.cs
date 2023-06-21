namespace Doggo.Domain.Entities.Base.JobRequestEntity;

using JobRequest;

public interface IJobRequestOwnedEntity
{
    public Guid JobRequestId { get; set; }

    public JobRequest JobRequest { get; set; }
}