namespace Doggo.Application.Requests.Commands.Walker.DeleteWalkerCommand;

using Domain.Results;
using MediatR;

public record DeleteWalkerCommand(Guid WalkerId) : IRequest<CommonResult>;