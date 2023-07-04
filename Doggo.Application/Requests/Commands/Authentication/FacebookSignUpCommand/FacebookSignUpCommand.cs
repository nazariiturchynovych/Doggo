namespace Doggo.Application.Requests.Commands.Authentication.FacebookSignUpCommand;

using Domain.Results;
using MediatR;

public record FacebookSignUpCommand(string AccessToken) : IRequest<CommonResult>;