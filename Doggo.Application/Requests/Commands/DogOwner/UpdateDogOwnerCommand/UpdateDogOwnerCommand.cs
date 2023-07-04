namespace Doggo.Application.Requests.Commands.DogOwner.UpdateDogOwnerCommand;

using Domain.Results;
using MediatR;

public record UpdateDogOwnerCommand(Guid DogOwnerId, string? Address, string? District) : IRequest<CommonResult>;