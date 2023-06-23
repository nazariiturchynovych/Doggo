namespace Doggo.Application.Validators.Walker;

using FluentValidation;
using Requests.Commands.Walker;

public class DeleteWalkerCommandValidator : AbstractValidator<DeleteWalkerCommand>
{
    public DeleteWalkerCommandValidator()
    {
        RuleFor(x => x.WalkerId).NotEmpty();
    }
}