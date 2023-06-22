// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Domain.DTO.Job;

using Dog;
using Enums;
using JobRequest;

public record GetJobDto(
    Guid Id,
    Guid WalkerId,
    Guid DogOwnerId,
    Guid DogId,
    string Comment,
    decimal Salary,
    JobStatus Status,
    GetDogDto GetDogDto,
    GetJobRequestDto JobRequestDto);