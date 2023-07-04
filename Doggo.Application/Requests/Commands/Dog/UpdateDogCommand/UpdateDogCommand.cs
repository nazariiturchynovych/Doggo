namespace Doggo.Application.Requests.Commands.Dog.UpdateDogCommand;

using Domain.Results;
using MediatR;

public record UpdateDogCommand(
    Guid DogId,
    double? Weight,
    string? Description,
    double? Age,
    string? Name) : IRequest<CommonResult>;