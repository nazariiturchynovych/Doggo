namespace Doggo.Application.Requests.Commands.Job.RejectJobCommand;

using FluentValidation;

public class RejectJobCommandValidator : AbstractValidator<RejectJobCommand>
{
    public RejectJobCommandValidator()
    {
        RuleFor(x => x.JobId).NotEmpty();
    }
}