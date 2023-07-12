namespace Doggo.Application.Requests.Commands.PossibleSchedule.DeletePossibleScheduleCommand;

using Base;
using Domain.Results;

public record DeletePossibleScheduleCommand(Guid PossibleScheduleId) : ICommand<CommonResult>;