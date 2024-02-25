namespace Doggo.Application.Requests.Commands.Job.CreateAndApplyJobCommand;

using FluentValidation;

public class CreateAndApplyJobCommandValidator : AbstractValidator<CreateAndApplyJobCommand>
{
    public CreateAndApplyJobCommandValidator()
    {
        RuleFor(x => x.Payment).NotEmpty();
        RuleFor(x => x.JobRequestId).NotEmpty();
        RuleFor(x => x.Payment).GreaterThan(5).LessThan(5000);
        RuleFor(x => x.Comment).MinimumLength(5).MaximumLength(150);

    }
}