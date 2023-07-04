namespace Doggo.Application.Requests.Commands.DogOwner.DeleteDogOwnerCommand;

using Domain.Results;
using MediatR;

public record DeleteDogOwnerCommand(Guid DogOwnerId) : IRequest<CommonResult>;