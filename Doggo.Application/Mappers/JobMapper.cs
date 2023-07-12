namespace Doggo.Application.Mappers;

using Domain.Entities.Job;
using Requests.Commands.Job.UpdateJobCommand;
using Responses;
using Responses.Job;

public static class JobMapper
{
    public static Job MapJobUpdateCommandToJob(this UpdateJobCommand command, Job job)
    {
        job.Comment = command.Comment ?? job.Comment;
        job.Payment = command.Payment ?? job.Payment;
        job.ChangedDate = DateTime.UtcNow;
        return job;
    }

    public static JobResponse MapJobToJobResponse(this Job job)
    {
        return new JobResponse(
            job.Id,
            job.WalkerId,
            job.DogOwnerId,
            job.DogId,
            job.Comment,
            job.Payment,
            job.Status,
            job.Dog.MapDogToDogResponse(),
            job.JobRequest.MapJobRequestToJobRequestResponse());
    }

    public static PageOf<JobResponse> MapJobCollectionToPageOfJobResponse(this IReadOnlyCollection<Job> collection)
    {
        var collectionDto = collection.Select(job => job.MapJobToJobResponse()).ToList();

        return new PageOf<JobResponse>(collectionDto);
    }

    public static List<JobResponse> MapJobCollectionToListJobResponse(this IReadOnlyCollection<Job> collection)
    {
        return collection.Select(job => job.MapJobToJobResponse()).ToList();
    }
}