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
            job.Comment,
            job.JobRequest.MapJobRequestToGetJobRequestDto());
    }

    public static PageOfTDataDto<GetJobDto> MapJobCollectionToPageOJobDto(this IReadOnlyCollection<Job> collection)
    {
        var collectionDto = new List<GetJobDto>();

        foreach (var job in collection)
        {
            collectionDto.Add(job.MapJobToGetJobDto());
        }

        return new PageOfTDataDto<GetJobDto>(collectionDto);
    }
}