namespace Doggo.Application.Requests.Commands.Image.DeleteImageCommand;

using Domain.Results;
using MediatR;

public record DeleteImageCommand(Guid Id) : IRequest<CommonResult>;