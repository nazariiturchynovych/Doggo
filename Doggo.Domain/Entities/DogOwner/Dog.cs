#pragma warning disable CS8618
namespace Doggo.Domain.Entities.DogOwner;

using Base;
using JobRequest;

public class Dog : Entity
{
    public string Name { get; set; }

    public double Age { get; set; }

    public double? Weight { get; set; }

    public string Description { get; set; } //TODO add to db migrations

    public int DogOwnerId { get; set; }

    public DogOwner DogOwner { get; set; }

    public int JobRequestId { get; set; }

    public ICollection<JobRequest> JobRequest { get; set; }
}