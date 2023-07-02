namespace Doggo.Application.Mappers;

using Domain.DTO.JobRequest;
using Domain.Entities.JobRequest.Schedules;

public static class RequiredScheduleMapper
{
    public static GetRequiredScheduleDto MapRequiredScheduleToGetRequiredScheduleDto(this RequiredSchedule requiredSchedule)
    {
        return new GetRequiredScheduleDto(
            requiredSchedule.From,
            requiredSchedule.To,
            requiredSchedule.IsRegular);
    }
}