namespace Doggo.Domain.Entities.JobRequest.Schedules;

using Doggo.Domain.Entities.Base.JobRequest;

public class RequiredSchedule : JobRequestOwnedEntity
{
    public DayOfWeek DayOfWeek { get; set; }

    public TimeOnly? From { get; set; }
    
    public TimeOnly? To { get; set; }

    public bool IsRegular { get; set; }
}