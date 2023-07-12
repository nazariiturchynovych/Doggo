namespace Doggo.Application.Requests.Commands.DogOwner.CreateDogOwnerCommand;

using Base;
using Domain.Results;

public record CreateDogOwnerCommand(string Address, string District) : ICommand<CommonResult>;