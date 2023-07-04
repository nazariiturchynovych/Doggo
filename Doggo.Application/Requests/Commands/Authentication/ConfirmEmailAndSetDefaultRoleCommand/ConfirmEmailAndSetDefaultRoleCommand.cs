namespace Doggo.Application.Requests.Commands.Authentication.ConfirmEmailAndSetDefaultRoleCommand;

using Domain.Results;
using MediatR;

public record ConfirmEmailAndSetDefaultRoleCommand
    (Guid UserId, string Token) : IRequest<CommonResult>;