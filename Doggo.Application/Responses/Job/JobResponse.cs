// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Application.Responses.Job;

using Dog;
using Domain.Enums;
using JobRequest;

public record JobResponse(
    Guid Id,
    Guid WalkerId,
    Guid DogOwnerId,
    Guid DogId,
    string Comment,
    decimal Salary,
    JobStatus Status,
    DogResponse DogResponse,
    JobRequestResponse JobRequestResponse);