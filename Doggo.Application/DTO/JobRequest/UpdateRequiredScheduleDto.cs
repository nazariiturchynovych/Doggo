namespace Doggo.Domain.DTO.JobRequest;

public record UpdateRequiredScheduleDto( DateTime? From, DateTime? To, bool? IsRegular);