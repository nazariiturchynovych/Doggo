#pragma warning disable CS8618
namespace Doggo.Domain.Entities.Job;

using Base.AuditableDateEntity;
using Dog;
using DogOwner;
using Enums;
using JobRequest;
using Walker;

public class Job : AuditableDateEntity
{
    public Guid DogOwnerId { get; set; }

    public DogOwner DogOwner { get; set; }

    public Guid WalkerId { get; set; }

    public Walker Walker { get; set; }

    public Guid JobRequestId { get; set; }

    public JobRequest JobRequest { get; set; }

    public Guid DogId { get; set; }

    public Dog Dog { get; set; }

    public string Comment { get; set; }

    public decimal Salary { get; set; }

    public JobStatus Status { get; set; }
}