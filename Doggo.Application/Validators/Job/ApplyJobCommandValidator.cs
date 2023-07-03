namespace Doggo.Application.Validators.Job;

using FluentValidation;
using Requests.Commands.Job;

public class ApplyJobCommandValidator : AbstractValidator<ApplyJobCommand>
{
    public ApplyJobCommandValidator()
    {
        RuleFor(x => x.JobId).NotEmpty();
    }
}