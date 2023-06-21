namespace Doggo.Application.Mappers;

using Domain.DTO;
using Domain.DTO.Job;
using Domain.Entities.Job;
using Requests.Commands.Job;

public static class JobMapper
{
    public static Job MapJobUpdateCommandToJob(this UpdateJobCommand command, Job job)
    {
        job.Comment = command.Comment ?? job.Comment;
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
            job.Salary,
            job.JobRequest.MapJobRequestToGetJobRequestDto());
    }

    public static PageOfTDataDto<GetJobDto> MapJobCollectionToPageOJobDto(this IReadOnlyCollection<Job> collection)
    {
        var collectionDto = collection.Select(job => job.MapJobToGetJobDto()).ToList();

        return new PageOfTDataDto<GetJobDto>(collectionDto);
    }
}