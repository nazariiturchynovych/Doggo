namespace Doggo.Application.Validators.Job;

using FluentValidation;
using Requests.Commands.Job;

public class DeclineJobCommandValidator : AbstractValidator<DeclineJobCommand>
{
    public DeclineJobCommandValidator()
    {
        RuleFor(x => x.JobId).NotEmpty();
    }
}