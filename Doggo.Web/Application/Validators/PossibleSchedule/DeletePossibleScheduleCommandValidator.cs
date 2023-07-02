namespace Doggo.Application.Validators.PossibleSchedule;

using FluentValidation;
using Requests.Commands.Walker.PossibleSchedule;

public class DeletePossibleScheduleCommandValidator : AbstractValidator<DeletePossibleScheduleCommand>
{
    public DeletePossibleScheduleCommandValidator()
    {
        RuleFor(x => x.PossibleScheduleId).NotEmpty();
    }
}