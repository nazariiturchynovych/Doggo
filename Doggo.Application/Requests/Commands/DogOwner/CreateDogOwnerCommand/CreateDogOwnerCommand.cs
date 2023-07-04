namespace Doggo.Application.Requests.Commands.DogOwner.CreateDogOwnerCommand;

using Domain.Results;
using MediatR;

public record CreateDogOwnerCommand(string Address, string District) : IRequest<CommonResult>;