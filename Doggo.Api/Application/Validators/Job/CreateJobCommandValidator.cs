namespace Doggo.Application.Validators.Job;

using FluentValidation;
using Requests.Commands.Job;

public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
{
    public CreateJobCommandValidator()
    {
        RuleFor(x => x.DogOwnerId).NotEmpty();
        RuleFor(x => x.DogId).NotEmpty();
        RuleFor(x => x.WalkerId).NotEmpty();
        RuleFor(x => x.JobRequestId).NotEmpty();
        RuleFor(x => x.Salary).GreaterThan(5).LessThan(5000);
        RuleFor(x => x.Comment).MinimumLength(5).MaximumLength(150);

    }
}