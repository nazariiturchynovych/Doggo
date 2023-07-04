namespace Doggo.Application.Requests.Commands.JobRequest.DeleteJobRequestCommand;

using FluentValidation;

public class DeleteJobRequestCommandValidator : AbstractValidator<DeleteJobRequestCommand>
{
    public DeleteJobRequestCommandValidator()
    {
        RuleFor(x => x.JobRequestId).NotEmpty();
    }
}