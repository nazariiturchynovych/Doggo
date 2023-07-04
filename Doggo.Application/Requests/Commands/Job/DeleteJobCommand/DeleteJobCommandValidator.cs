namespace Doggo.Application.Requests.Commands.Job.DeleteJobCommand;

using FluentValidation;

public class DeleteJobCommandValidator : AbstractValidator<DeleteJobCommand>
{
    public DeleteJobCommandValidator()
    {
        RuleFor(x => x.JobId).NotEmpty();
    }
}