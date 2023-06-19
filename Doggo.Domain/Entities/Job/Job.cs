#pragma warning disable CS8618
namespace Doggo.Domain.Entities.Job;

using Doggo.Domain.Entities.Base.AuditableDateEntity;
using Doggo.Domain.Entities.DogOwner;
using Doggo.Domain.Entities.JobRequest;
using Doggo.Domain.Entities.Walker;
using Doggo.Domain.Enums;

public class Job : AuditableDateEntity
{
    public int DogOwnerId { get; set; }

    public DogOwner DogOwner { get; set; }

    public int WalkerId { get; set; }

    public Walker Walker { get; set; }

    public int JobRequestId { get; set; }
    public JobRequest JobRequest { get; set; }

    public string Comment { get; set; }

    public JobStatus Status { get; set; }
}