namespace Doggo.Application.UnitTests.TestUtils.Factory;

using Domain.Entities.Dog;
using Domain.Entities.JobRequest;
using static Constants.Constants;


public static partial class Factory
{
    public static class DogFactory
    {
        public static Dog CreateDog()
        {
            return new Dog()
            {
                Id = ValidDog.Id,
                Age = ValidDog.Age,
                Weight = ValidDog.Weight,
                Name = ValidDog.Name,
                Description = ValidDog.Description,
                DogOwnerId = ValidDogOwner.Id,
            };
        }

        public static Dog CreateDogWithAllIncludes()
        {
            var dog = CreateDog();
            dog.DogOwner = DogOwnerFactory.CreateDogOwner();
            dog.JobRequests = new List<JobRequest>() {JobRequestFactory.CreateJobRequest()};
            return dog;
        }
    }
}