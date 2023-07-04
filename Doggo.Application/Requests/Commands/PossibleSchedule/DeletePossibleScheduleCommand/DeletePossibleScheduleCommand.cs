namespace Doggo.Application.Requests.Commands.PossibleSchedule.DeletePossibleScheduleCommand;

using Domain.Results;
using MediatR;

public record DeletePossibleScheduleCommand(Guid PossibleScheduleId) : IRequest<CommonResult>;