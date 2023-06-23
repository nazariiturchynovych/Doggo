namespace Doggo.Application.Validators.Job;

using FluentValidation;
using Requests.Commands.Job;

public class UpdateJobCommandValidator : AbstractValidator<UpdateJobCommand>
{
    public UpdateJobCommandValidator()
    {
        When(
            x => x.Comment is not null,
            () => RuleFor(x => x.Comment).MinimumLength(5).MaximumLength(200));

        When(x => x.Salary is not null,
            () => RuleFor(x => x.Salary).GreaterThan(5).LessThan(5000));
    }
}