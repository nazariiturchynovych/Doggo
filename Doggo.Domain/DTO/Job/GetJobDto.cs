// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Domain.DTO.Job;

using JobRequest;

public record GetJobDto(
    Guid Id,
    Guid WalkerId,
    Guid DogOwnerId,
    Guid DogId,
    string Comment,
    decimal Salary,
    GetJobRequestDto JobRequestDto);