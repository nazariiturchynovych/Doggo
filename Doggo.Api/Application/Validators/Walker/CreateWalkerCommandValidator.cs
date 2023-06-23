namespace Doggo.Application.Validators.Walker;

using FluentValidation;
using Requests.Commands.Walker;

public class CreateWalkerCommandValidator : AbstractValidator<CreateWalkerCommand>
{
    public CreateWalkerCommandValidator()
    {
        RuleFor(x => x.Skills).MinimumLength(5);
        RuleFor(x => x.About).MinimumLength(5);
    }
}