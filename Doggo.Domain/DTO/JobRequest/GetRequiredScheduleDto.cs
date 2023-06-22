// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Domain.DTO.JobRequest;

public record GetRequiredScheduleDto( DateTime From, DateTime To, bool IsRegular);