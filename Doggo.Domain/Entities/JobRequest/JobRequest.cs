#pragma warning disable CS8618
namespace Doggo.Domain.Entities.JobRequest;

using Base.AuditableDateEntity;
using Constants;
using Dog;
using DogOwner;
using Job;
using Schedule;

public class JobRequest : AuditableDateEntity
{
    public JobRequest() => ValidFrom = CreatedDate.AddMinutes(JobRequestConstants.ValidAfter);

    public int RequiredAge { get; set; }

    public bool IsPersonalIdentifierRequired { get; set; }

    public RequiredSchedule RequiredSchedule { get; set; }

    public string Description { get; set; }

    public Guid DogOwnerId { get; set; }
    public DogOwner DogOwner { get; set; }
    public decimal PaymentTo { get; set; }

    public bool HasAcceptedJob { get; set; }
    public DateTime ValidFrom { get; }

    public IReadOnlyCollection<Job> Jobs { get; set; }

    public Guid DogId { get; set; }

    public Dog Dog { get; set; }
}