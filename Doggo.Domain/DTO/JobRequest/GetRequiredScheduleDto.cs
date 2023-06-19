// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Domain.DTO.JobRequest;

public record GetRequiredScheduleDto(DayOfWeek DayOfWeek, TimeOnly From, TimeOnly To, bool IsRegular);