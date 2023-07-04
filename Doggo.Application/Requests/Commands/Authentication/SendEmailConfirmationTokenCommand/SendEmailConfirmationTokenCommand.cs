namespace Doggo.Application.Requests.Commands.Authentication.SendEmailConfirmationTokenCommand;

using Domain.Results;
using MediatR;

public record SendEmailConfirmationTokenCommand(string UserEmail) : IRequest<CommonResult>;