namespace Doggo.Application.Mappers;

using Domain.Entities.JobRequest.Schedule;
using DTO.JobRequest;

public static class RequiredScheduleMapper
{
    public static GetRequiredScheduleDto MapRequiredScheduleToGetRequiredScheduleDto(this RequiredSchedule requiredSchedule)
    {
        return new GetRequiredScheduleDto(
            requiredSchedule.From,
            requiredSchedule.To);
    }
}