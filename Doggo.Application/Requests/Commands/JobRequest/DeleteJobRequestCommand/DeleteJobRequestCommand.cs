namespace Doggo.Application.Requests.Commands.JobRequest.DeleteJobRequestCommand;

using Domain.Results;
using MediatR;

public record DeleteJobRequestCommand(Guid JobRequestId) : IRequest<CommonResult>;