// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Domain.DTO.Walker.PossibleSchedule;

public record GetPossibleScheduleDto(
    int Id,
    TimeOnly From,
    TimeOnly To,
    DayOfWeek DayOfWeek);