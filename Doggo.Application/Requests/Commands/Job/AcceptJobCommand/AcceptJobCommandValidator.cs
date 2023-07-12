namespace Doggo.Application.Requests.Commands.Job.AcceptJobCommand;

using FluentValidation;

public class AcceptJobCommandValidator : AbstractValidator<AcceptJobCommand>
{
    public AcceptJobCommandValidator()
    {
        RuleFor(x => x.JobId).NotEmpty();
    }
}