namespace Doggo.Application.UnitTests.TestUtils.Factory;

using Domain.Entities.Job;
using static Constants.Constants;

public static partial class Factory
{
    public static class JobFactory
    {
        public static Job CreateJob()
        {
            return new Job()
            {
                Id = ValidJob.Id,
                CreatedDate = ValidJob.CreatedDate,
                Payment = ValidJob.Payment,
                Comment = ValidJob.Comment,
                Status = ValidJob.Status,
                DogOwnerId = ValidDogOwner.Id,
                DogId = ValidDog.Id,
                WalkerId = ValidWalker.Id,
                JobRequestId = ValidJobRequest.Id
            };
        }

        public static Job CreateJobWithAllIncludes()
        {
            var job = CreateJob();
            job.DogOwner = DogOwnerFactory.CreateDogOwner();
            job.Dog = DogFactory.CreateDog();
            job.JobRequest = JobRequestFactory.CreateJobRequest();
            job.Walker = WalkerFactory.CreateWalker();
            return job;
        }
    }
}