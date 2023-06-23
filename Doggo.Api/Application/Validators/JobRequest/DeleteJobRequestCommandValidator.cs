namespace Doggo.Application.Validators.JobRequest;

using FluentValidation;
using Requests.Commands.JobRequest;

public class DeleteJobRequestCommandValidator : AbstractValidator<DeleteJobRequestCommand>
{
    public DeleteJobRequestCommandValidator()
    {
        RuleFor(x => x.JobRequestId).NotEmpty();
    }
}