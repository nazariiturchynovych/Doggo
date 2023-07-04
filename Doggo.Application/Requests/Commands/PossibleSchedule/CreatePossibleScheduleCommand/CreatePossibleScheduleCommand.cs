namespace Doggo.Application.Requests.Commands.PossibleSchedule.CreatePossibleScheduleCommand;

using Domain.Results;
using MediatR;

public record CreatePossibleScheduleCommand(Guid WalkerId, DateTime From, DateTime To) : IRequest<CommonResult>;