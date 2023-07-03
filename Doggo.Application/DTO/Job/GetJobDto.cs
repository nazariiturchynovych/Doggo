// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Application.DTO.Job;

using Dog;
using Domain.Enums;
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