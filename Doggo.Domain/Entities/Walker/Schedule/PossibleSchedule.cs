#pragma warning disable CS8618
namespace Doggo.Domain.Entities.Walker.Schedule;

using Base;

public class PossibleSchedule : Entity
{
    public Guid WalkerId { get; set; }

    public Walker Walker { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }

}
