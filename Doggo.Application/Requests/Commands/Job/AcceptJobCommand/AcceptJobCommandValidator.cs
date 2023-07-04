namespace Doggo.Application.Requests.Commands.Job.ApplyJobCommand;

using FluentValidation;

public class AcceptJobCommandValidator : AbstractValidator<AcceptJobCommand>
{
    public AcceptJobCommandValidator()
    {
        RuleFor(x => x.JobId).NotEmpty();
    }
}