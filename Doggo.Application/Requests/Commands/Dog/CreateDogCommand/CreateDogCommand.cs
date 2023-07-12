namespace Doggo.Application.Requests.Commands.Dog.CreateDogCommand;

using Base;
using Domain.Results;

public record CreateDogCommand(
    string Name,
    double Age,
    double? Weight,
    Guid DogOwnerId,
    string Description) : ICommand<CommonResult>;