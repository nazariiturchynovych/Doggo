namespace Doggo.Application.Mappers;

using Domain.Entities.Job;
using DTO;
using DTO.Job;
using Requests.Commands.Job;
using Requests.Commands.Job.UpdateJobCommand;

public static class JobMapper
{
    public static Job MapJobUpdateCommandToJob(this UpdateJobCommand command, Job job)
    {
        job.Comment = command.Comment ?? job.Comment;
        job.Payment = command.Salary ?? job.Payment;
        job.ChangedDate = DateTime.UtcNow;
        return job;
    }

    public static GetJobDto MapJobToGetJobDto(this Job job)
    {
        return new GetJobDto(
            job.Id,
            job.WalkerId,
            job.DogOwnerId,
            job.DogId,
            job.Comment,
            job.Payment,
            job.Status,
            job.Dog.MapDogToGetDogDto(),
            job.JobRequest.MapJobRequestToGetJobRequestDto());
    }

    public static PageOfTDataDto<GetJobDto> MapJobCollectionToPageOJobDto(this IReadOnlyCollection<Job> collection)
    {
        var collectionDto = collection.Select(job => job.MapJobToGetJobDto()).ToList();

        return new PageOfTDataDto<GetJobDto>(collectionDto);
    }
}