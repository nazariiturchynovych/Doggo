namespace Doggo.Application.Requests.Commands.Walker.DeleteWalkerCommand;

using FluentValidation;

public class DeleteWalkerCommandValidator : AbstractValidator<DeleteWalkerCommand>
{
    public DeleteWalkerCommandValidator()
    {
        RuleFor(x => x.WalkerId).NotEmpty();
    }
}