namespace Doggo.Application.Validators.PossibleSchedule;

using FluentValidation;
using Requests.Commands.Walker.PossibleSchedule;

public class CreatePossibleScheduleCommandValidator : AbstractValidator<CreatePossibleScheduleCommand>
{
    public CreatePossibleScheduleCommandValidator()
    {
        RuleFor(x => x.From).GreaterThan(DateTime.UtcNow);

        RuleFor(x => x.To).GreaterThan(x => x.From);
    }
}