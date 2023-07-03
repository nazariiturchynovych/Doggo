// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Application.DTO.Walker.PossibleSchedule;

public record GetPossibleScheduleDto(
    Guid Id,
    DateTime From,
    DateTime To);