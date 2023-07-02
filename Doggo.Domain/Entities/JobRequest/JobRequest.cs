#pragma warning disable CS8618
namespace Doggo.Domain.Entities.JobRequest;

using Base.AuditableDateEntity;
using Dog;
using DogOwner;
using Job;
using Schedules;

public class JobRequest : AuditableDateEntity
{
    public int RequiredAge { get; set; }

    public bool IsPersonalIdentifierRequired { get; set; }

    public RequiredSchedule RequiredSchedule { get; set; }

    public string Description { get; set; }

    public Guid DogOwnerId { get; set; }

    public DogOwner DogOwner { get; set; }

    public decimal Salary { get; set; }

    public Job Job { get; set; }

    public Guid DogId { get; set; }

    public Dog Dog { get; set; }
}