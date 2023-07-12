namespace Doggo.Application.Requests.Commands.PossibleSchedule.CreatePossibleScheduleCommand;

using Base;
using Domain.Results;

public record CreatePossibleScheduleCommand(Guid WalkerId, DateTime From, DateTime To) : ICommand<CommonResult>;