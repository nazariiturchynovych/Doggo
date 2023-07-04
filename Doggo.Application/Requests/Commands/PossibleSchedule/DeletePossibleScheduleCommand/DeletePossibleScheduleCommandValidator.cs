namespace Doggo.Application.Requests.Commands.PossibleSchedule.DeletePossibleScheduleCommand;

using FluentValidation;

public class DeletePossibleScheduleCommandValidator : AbstractValidator<DeletePossibleScheduleCommand>
{
    public DeletePossibleScheduleCommandValidator()
    {
        RuleFor(x => x.PossibleScheduleId).NotEmpty();
    }
}