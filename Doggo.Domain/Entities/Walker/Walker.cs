// ReSharper disable CollectionNeverUpdated.Global
#pragma warning disable CS8618
namespace Doggo.Domain.Entities.Walker;

using Base;
using Job;
using Schedule;
using User;

public class Walker : Entity
{
    public Guid UserId { get; set; }

    public User User { get; set; }

    public string About { get; set; }

    public string Skills { get; set; }
    public ICollection<PossibleSchedule> PossibleSchedules { get; set; }
    public ICollection<Job> Jobs { get; set; }
}