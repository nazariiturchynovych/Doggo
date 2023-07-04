namespace Doggo.Application.Requests.Commands.User.DeleteUserCommand;

using Domain.Results;
using MediatR;

public record DeleteUserCommand(Guid UserId) : IRequest<CommonResult>;