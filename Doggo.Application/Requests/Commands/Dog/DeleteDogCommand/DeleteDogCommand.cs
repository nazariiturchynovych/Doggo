namespace Doggo.Application.Requests.Commands.Dog.DeleteDogCommand;

using Base;
using Domain.Results;

public record DeleteDogCommand(Guid DogId) : ICommand<CommonResult>;