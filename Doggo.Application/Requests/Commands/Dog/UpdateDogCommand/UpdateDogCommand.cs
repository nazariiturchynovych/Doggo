namespace Doggo.Application.Requests.Commands.Dog.UpdateDogCommand;

using Base;
using Domain.Results;

public record UpdateDogCommand(
    Guid DogId,
    double? Weight,
    string? Description,
    double? Age,
    string? Name) : ICommand<CommonResult>;