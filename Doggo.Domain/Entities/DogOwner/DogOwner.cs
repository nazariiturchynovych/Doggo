// ReSharper disable CollectionNeverUpdated.Global

#pragma warning disable CS8618
namespace Doggo.Domain.Entities.DogOwner;

using Base;
using Dog;
using Job;
using JobRequest;
using User;

public class DogOwner : Entity
{
    public Guid UserId { get; set; }

    public User User { get; set; }

    public string Address { get; set; }

    public string District { get; set; }

    public ICollection<Dog> Dogs { get; set; }

    public ICollection<JobRequest> JobRequests { get; set; }

    public ICollection<Job> Jobs { get; set; }
}