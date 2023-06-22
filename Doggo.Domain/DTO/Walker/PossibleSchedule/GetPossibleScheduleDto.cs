// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Domain.DTO.Walker.PossibleSchedule;

public record GetPossibleScheduleDto(
    Guid Id,
    DateTime From,
    DateTime To);