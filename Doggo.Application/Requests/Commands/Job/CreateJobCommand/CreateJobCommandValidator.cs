namespace Doggo.Application.Requests.Commands.Job.CreateJobCommand;

using FluentValidation;

public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
{
    public CreateJobCommandValidator()
    {
        RuleFor(x => x.DogOwnerId).NotEmpty();
        RuleFor(x => x.DogId).NotEmpty();
        RuleFor(x => x.Payment).NotEmpty();
        RuleFor(x => x.JobRequestId).NotEmpty();
        RuleFor(x => x.Payment).GreaterThan(5).LessThan(5000);
        RuleFor(x => x.Comment).MinimumLength(5).MaximumLength(150);

    }
}