namespace Doggo.Application.Requests.Commands.Authentication.ConfirmResetPasswordCommand;

using Domain.Results;
using MediatR;

public record ConfirmResetPasswordCommand(string Token, Guid UserId, string NewPassword) : IRequest<CommonResult>;