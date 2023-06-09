#pragma warning disable CS8618
namespace Doggo.Domain.Entities.Base.JobRequestEntity;

using Entities.JobRequest;
using JobRequest;

public abstract class JobRequestOwnedEntity : Entity, IJobRequestOwnedEntity
{
    public int JobRequestId { get; set; }

    public JobRequest JobRequest { get; set; }
}