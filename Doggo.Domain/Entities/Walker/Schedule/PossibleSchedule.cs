#pragma warning disable CS8618
namespace Doggo.Domain.Entities.Walker.Schedule;

using Base;

public class PossibleSchedule : Entity
{
    public int WalkerId { get; set; }

    public Walker Walker { get; set; }

    public TimeOnly From { get; set; }

    public TimeOnly To { get; set; }

    public DayOfWeek DayOfWeek { get; set; }
}
