#pragma warning disable CS0108, CS0114
#pragma warning disable CS8618
namespace Doggo.Domain.Entities.JobRequest.Schedules;

using Base.JobRequestEntity;

public class RequiredSchedule : JobRequestOwnedEntity
{
    public DayOfWeek DayOfWeek { get; set; }

    public TimeOnly From { get; set; }

    public TimeOnly To { get; set; }

    public bool IsRegular { get; set; }
}