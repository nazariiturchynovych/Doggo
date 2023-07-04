namespace Doggo.Application.Requests.Commands.Walker.CreateWalkerCommand;

using Domain.Results;
using MediatR;

public record CreateWalkerCommand(string Skills, string About) : IRequest<CommonResult>;