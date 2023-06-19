namespace Doggo.Domain.DTO.JobRequest;

public record UpdateRequiredScheduleDto(DayOfWeek? DayOfWeek, TimeOnly? From, TimeOnly? To, bool? IsRegular);