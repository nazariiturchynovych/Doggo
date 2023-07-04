namespace Doggo.Application.Requests.Commands.Dog.DeleteDogCommand;

using Domain.Results;
using MediatR;

public record DeleteDogCommand(Guid DogId) : IRequest<CommonResult>;