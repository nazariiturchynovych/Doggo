namespace Doggo.Domain.DTO.JobRequest;

public record RequiredScheduleDto(DayOfWeek DayOfWeek, TimeOnly From, TimeOnly To, bool IsRegular);