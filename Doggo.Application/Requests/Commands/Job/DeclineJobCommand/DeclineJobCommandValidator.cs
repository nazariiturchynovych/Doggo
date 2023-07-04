namespace Doggo.Application.Requests.Commands.Job.DeclineJobCommand;

using FluentValidation;

public class DeclineJobCommandValidator : AbstractValidator<DeclineJobCommand>
{
    public DeclineJobCommandValidator()
    {
        RuleFor(x => x.JobId).NotEmpty();
    }
}