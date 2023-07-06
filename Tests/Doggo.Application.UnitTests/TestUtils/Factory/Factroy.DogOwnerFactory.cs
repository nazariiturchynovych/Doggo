namespace Doggo.Application.UnitTests.TestUtils.Factory;

using Domain.Entities.Dog;
using Domain.Entities.DogOwner;
using Domain.Entities.Job;
using Domain.Entities.JobRequest;
using static Constants.Constants;

public static partial class Factory
{
    public static class DogOwnerFactory
    {
        public static DogOwner CreateDogOwner()
        {
            return new DogOwner()
            {
                Id = ValidDogOwner.Id,
                Address = ValidDogOwner.Address,
                District = ValidDogOwner.District,
                UserId = ValidUser.Id,
            };
        }

        public static DogOwner CreateDogOwnerWithAllIncludes()
        {
            var dogOwner = CreateDogOwner();
            dogOwner.User = UserFactory.CreateUser();
            dogOwner.Dogs = new List<Dog>() {DogFactory.CreateDog()};
            dogOwner.Jobs = new List<Job>() {JobFactory.CreateJob()};
            dogOwner.JobRequests = new List<JobRequest>() {JobRequestFactory.CreateJobRequest()};
            return dogOwner;
        }
    }
}