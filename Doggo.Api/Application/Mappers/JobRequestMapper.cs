namespace Doggo.Application.Mappers;

using Domain.DTO;
using Domain.DTO.JobRequest;
using Domain.Entities.JobRequest;
using Requests.Commands.JobRequest;

public static class JobRequestMapper
{
    public static JobRequest MapUpdateJobRequestCommandToJobRequest(this UpdateJobRequestCommand command, JobRequest jobRequest)
    {
        jobRequest.IsPersonalIdentifierRequired = command.IsPersonalIdentifierRequired ?? jobRequest.IsPersonalIdentifierRequired;
        jobRequest.RequiredAge = command.RequiredAge ?? jobRequest.RequiredAge;
        jobRequest.Description = command.Description ?? jobRequest.Description;

        jobRequest.RequiredSchedule.From = command.RequiredScheduleDto!.From ?? jobRequest.RequiredSchedule.From;
        jobRequest.RequiredSchedule.To = command.RequiredScheduleDto!.To ?? jobRequest.RequiredSchedule.To;
        jobRequest.RequiredSchedule.DayOfWeek = command.RequiredScheduleDto!.DayOfWeek ?? jobRequest.RequiredSchedule.DayOfWeek;
        jobRequest.RequiredSchedule.IsRegular = command.RequiredScheduleDto!.IsRegular ?? jobRequest.RequiredSchedule.IsRegular;

        return jobRequest;
    }

    public static GetJobRequestDto MapJobRequestToGetJobRequestDto(this JobRequest jobRequest)
    {
        return new GetJobRequestDto(
            jobRequest.Id,
            jobRequest.IsPersonalIdentifierRequired,
            jobRequest.Description,
            jobRequest.RequiredAge,
            jobRequest.RequiredSchedule.MapRequiredScheduleToGetRequiredScheduleDto());
    }


    public static PageOfTDataDto<GetJobRequestDto> MapJobRequestCollectionToPageOJobRequestsDto(this IReadOnlyCollection<JobRequest> collection)
    {
        var collectionDto = new List<GetJobRequestDto>();

        foreach (var walker in collection)
        {
            collectionDto.Add(walker.MapJobRequestToGetJobRequestDto());
        }
        return new PageOfTDataDto<GetJobRequestDto>(collectionDto);
    }
}