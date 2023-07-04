namespace Doggo.Application.Requests.Commands.Authentication.ApproveUserCommand;

using Domain.Results;
using MediatR;

public record ApproveUserCommand(int UserId) : IRequest<CommonResult>;