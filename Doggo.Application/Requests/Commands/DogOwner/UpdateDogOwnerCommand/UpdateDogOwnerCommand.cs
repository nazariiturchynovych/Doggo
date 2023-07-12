namespace Doggo.Application.Requests.Commands.DogOwner.UpdateDogOwnerCommand;

using Base;
using Domain.Results;

public record UpdateDogOwnerCommand(Guid DogOwnerId, string? Address, string? District) : ICommand<CommonResult>;