namespace Doggo.Application.Validators.Job;

using FluentValidation;
using Requests.Commands.Job;

public class DeleteJobCommandValidator : AbstractValidator<DeleteJobCommand>
{
    public DeleteJobCommandValidator()
    {
        RuleFor(x => x.JobId).NotEmpty();
    }
}