#pragma warning disable CS0108, CS0114
#pragma warning disable CS8618
namespace Doggo.Domain.Entities.JobRequest.Schedule;

using Base.JobRequestEntity;

public class RequiredSchedule : JobRequestOwnedEntity
{
    public DateTime From { get; set; }

    public DateTime To { get; set; }

}