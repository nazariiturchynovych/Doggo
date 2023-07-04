namespace Doggo.Application.Requests.Commands.Dog.CreateDogCommand;

using Domain.Results;
using MediatR;

public record CreateDogCommand(
    string Name,
    double Age,
    double? Weight,
    Guid DogOwnerId,
    string Description) : IRequest<CommonResult>;