namespace Doggo.Application.Requests.Commands.PossibleSchedule.CreatePossibleScheduleCommand;

using FluentValidation;

public class CreatePossibleScheduleCommandValidator : AbstractValidator<CreatePossibleScheduleCommand>
{
    public CreatePossibleScheduleCommandValidator()
    {
        RuleFor(x => x.From).GreaterThan(DateTime.UtcNow);

        RuleFor(x => x.To).GreaterThan(x => x.From);
    }
}