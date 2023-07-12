namespace Doggo.Application.Requests.Commands.DogOwner.DeleteDogOwnerCommand;

using Base;
using Domain.Results;

public record DeleteDogOwnerCommand(Guid DogOwnerId) : ICommand<CommonResult>;