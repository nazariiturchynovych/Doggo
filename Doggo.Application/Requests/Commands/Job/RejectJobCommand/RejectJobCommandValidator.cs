namespace Doggo.Application.Requests.Commands.Job.DeclineJobCommand;

using FluentValidation;

public class RejectJobCommandValidator : AbstractValidator<RejectJobCommand>
{
    public RejectJobCommandValidator()
    {
        RuleFor(x => x.JobId).NotEmpty();
    }
}