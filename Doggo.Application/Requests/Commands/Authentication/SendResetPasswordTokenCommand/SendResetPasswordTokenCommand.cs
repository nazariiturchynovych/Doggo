namespace Doggo.Application.Requests.Commands.Authentication.SendResetPasswordTokenCommand;

using Domain.Results;
using MediatR;

public record SendResetPasswordTokenCommand(string Email)
    : IRequest<CommonResult>;