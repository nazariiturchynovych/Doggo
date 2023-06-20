namespace Doggo.Domain.DTO.Job;

using JobRequest;

public record GetJobDto(int Id, string Comment, GetJobRequestDto JobRequestDto);