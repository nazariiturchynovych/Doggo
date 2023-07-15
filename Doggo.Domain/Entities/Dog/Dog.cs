// ReSharper disable CollectionNeverUpdated.Global
#pragma warning disable CS8618
namespace Doggo.Domain.Entities.Dog;

using Base;
using DogOwner;
using JobRequest;

public class Dog : Entity
{
    public string Name { get; set; }

    public double Age { get; set; }

    public double? Weight { get; set; }

    public string Description { get; set; }

    public Guid DogOwnerId { get; set; }

    public DogOwner? DogOwner { get; set; }

    public ICollection<JobRequest> JobRequests { get; set; }
}