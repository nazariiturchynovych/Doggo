namespace Doggo.Application.Mappers;

using Domain.Entities.JobRequest;
using Requests.Commands.JobRequest.UpdateJobRequestCommand;
using Responses;
using Responses.JobRequest;

public static class JobRequestMapper
{
    public static JobRequest MapUpdateJobRequestCommandToJobRequest(this UpdateJobRequestCommand command, JobRequest jobRequest)
    {
        jobRequest.IsPersonalIdentifierRequired = command.IsPersonalIdentifierRequired ?? jobRequest.IsPersonalIdentifierRequired;
        jobRequest.RequiredAge = command.RequiredAge ?? jobRequest.RequiredAge;
        jobRequest.Description = command.Description ?? jobRequest.Description;
        jobRequest.PaymentTo = command.PaymentTo ?? jobRequest.PaymentTo;
        jobRequest.ChangedDate = DateTime.UtcNow;

        jobRequest.RequiredSchedule.From = command.RequiredScheduleDto!.From ?? jobRequest.RequiredSchedule.From;
        jobRequest.RequiredSchedule.To = command.RequiredScheduleDto!.To ?? jobRequest.RequiredSchedule.To;

        return jobRequest;
    }

    public static JobRequestResponse MapJobRequestToJobRequestResponse(this JobRequest jobRequest)
    {
        return new JobRequestResponse(
            jobRequest.Id,
            jobRequest.IsPersonalIdentifierRequired,
            jobRequest.Description,
            jobRequest.RequiredAge,
            jobRequest.RequiredSchedule.MapRequiredScheduleToRequiredScheduleResponse());
    }


    public static PageOf<JobRequestResponse> MapJobRequestCollectionToPageOJobRequestsResponse(this IReadOnlyCollection<JobRequest> collection)
    {
        var collectionDto = new List<JobRequestResponse>();

        foreach (var jobRequest in collection)
        {
            collectionDto.Add(jobRequest.MapJobRequestToJobRequestResponse());
        }
        return new PageOf<JobRequestResponse>(collectionDto);
    }

    public static List<JobRequestResponse> MapJobRequestCollectionToListOJobRequestsResponse(this IReadOnlyCollection<JobRequest> collection)
    {
        var collectionDto = new List<JobRequestResponse>();

        foreach (var jobRequest in collection)
        {
            collectionDto.Add(jobRequest.MapJobRequestToJobRequestResponse());
        }

        return collectionDto;
    }
}