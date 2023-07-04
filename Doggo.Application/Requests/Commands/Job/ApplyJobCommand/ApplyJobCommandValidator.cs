namespace Doggo.Application.Requests.Commands.Job.ApplyJobCommand;

using FluentValidation;

public class ApplyJobCommandValidator : AbstractValidator<ApplyJobCommand>
{
    public ApplyJobCommandValidator()
    {
        RuleFor(x => x.JobId).NotEmpty();
    }
}