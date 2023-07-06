namespace Doggo.Application.UnitTests.TestUtils.Factory;

using Domain.Entities.Job;
using Domain.Entities.JobRequest;
using static Constants.Constants;

public static partial class Factory
{
    public static class JobRequestFactory
    {
        public static JobRequest CreateJobRequest()
        {
            return new JobRequest()
            {
                Id = ValidJobRequest.Id,
                Description = ValidJobRequest.Description,
                CreatedDate = ValidJobRequest.CreatedDate,
                PaymentTo = ValidJobRequest.PaymentTo,
                RequiredAge = ValidJobRequest.RequiredAge,
                RequiredSchedule = RequiredScheduleFactory.CreateRequiredSchedule(),
                HasAcceptedJob = ValidJobRequest.HasAcceptedJob,
                IsPersonalIdentifierRequired = ValidJobRequest.IsPersonalIdentifierRequired,
                DogOwnerId = ValidDogOwner.Id,
                DogId = ValidDog.Id,
            };
        }

        public static JobRequest CreateJobRequestWithAllIncludes()
        {
            var jobRequest = CreateJobRequest();
            jobRequest.DogOwner = DogOwnerFactory.CreateDogOwner();
            jobRequest.Dog = DogFactory.CreateDog();
            jobRequest.Jobs = new List<Job>() {JobFactory.CreateJob()};
            return jobRequest;
        }
    }
}