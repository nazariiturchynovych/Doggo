namespace Doggo.Application.Requests.Commands.Walker.UpdateWalkerCommand;

using Domain.Results;
using MediatR;

public record UpdateWalkerCommand(Guid WalkerId, string? Skills, string? About) : IRequest<CommonResult>;