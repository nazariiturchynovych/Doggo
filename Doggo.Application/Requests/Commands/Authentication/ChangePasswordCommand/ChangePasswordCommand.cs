namespace Doggo.Application.Requests.Commands.Authentication.ChangePasswordCommand;

using Domain.Results;
using MediatR;

public record ChangePasswordCommand
(
    string CurrentPassword,
    string NewPassword
) : IRequest<CommonResult>;