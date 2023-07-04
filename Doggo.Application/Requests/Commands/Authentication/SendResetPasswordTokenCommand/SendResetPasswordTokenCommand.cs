namespace Doggo.Application.Requests.Commands.Authentication.SendResetPasswordTokenCommand;

using Domain.Results;
using MediatR;

public record SendResetPasswordTokenCommand(string UserEmail, string NewPassword, string ConfirmPassword)
    : IRequest<CommonResult>;