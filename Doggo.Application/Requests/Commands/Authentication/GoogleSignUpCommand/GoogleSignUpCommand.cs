namespace Doggo.Application.Requests.Commands.Authentication.GoogleSignUpCommand;

using Domain.Results;
using MediatR;

public record GoogleSignUpCommand(string Credential) : IRequest<CommonResult>;