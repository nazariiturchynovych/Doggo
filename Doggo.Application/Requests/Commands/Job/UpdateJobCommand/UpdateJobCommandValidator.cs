namespace Doggo.Application.Requests.Commands.Job.UpdateJobCommand;

using FluentValidation;

public class UpdateJobCommandValidator : AbstractValidator<UpdateJobCommand>
{
    public UpdateJobCommandValidator()
    {
        When(
            x => x.Comment is not null,
            () => RuleFor(x => x.Comment).MinimumLength(5).MaximumLength(200));

        When(x => x.Payment is not null,
            () => RuleFor(x => x.Payment).GreaterThan(5).LessThan(5000));
    }
}