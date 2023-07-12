namespace Doggo.Application.Mappers;

using Domain.Entities.JobRequest.Schedule;
using Responses.JobRequest;

public static class RequiredScheduleMapper
{
    public static RequiredScheduleResponse MapRequiredScheduleToRequiredScheduleResponse(this RequiredSchedule requiredSchedule)
    {
        return new RequiredScheduleResponse(
            requiredSchedule.From,
            requiredSchedule.To);
    }
}