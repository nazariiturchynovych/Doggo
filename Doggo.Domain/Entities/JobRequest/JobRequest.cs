#pragma warning disable CS8618
namespace Doggo.Domain.Entities.JobRequest;

using Base.AuditableDateEntity;
using DogOwner;
using Job;
using Schedules;

public class JobRequest : AuditableDateEntity
{
    public int RequiredAge { get; set; }

    public bool IsPersonalIdentifierRequired { get; set; }

    public RequiredSchedule RequiredSchedule { get; set; }

    public int DogOwnerId { get; set; }

    public DogOwner DogOwner { get; set; }
    public int JobId { get; set; }
    public Job Job { get; set; }

    public int DogId { get; set; }

    public Dog Dog { get; set; }
}