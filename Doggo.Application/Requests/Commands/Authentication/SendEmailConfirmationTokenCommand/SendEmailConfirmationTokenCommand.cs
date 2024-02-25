namespace Doggo.Application.Requests.Commands.Authentication.SendEmailConfirmationTokenCommand;

using Domain.Results;
using MediatR;

public record SendEmailConfirmationTokenCommand(string Email) : IRequest<CommonResult>;