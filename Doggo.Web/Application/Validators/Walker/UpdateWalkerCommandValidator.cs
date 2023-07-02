namespace Doggo.Application.Validators.Walker;

using FluentValidation;
using Requests.Commands.Walker;

public class UpdateWalkerCommandValidator : AbstractValidator<UpdateWalkerCommand>
{
    public UpdateWalkerCommandValidator()
    {
        When(
            x => x.About is not null,
            () => RuleFor(x => x.About).MinimumLength(5));

        When(x => x.Skills is not null,
            () => RuleFor(x => x.Skills).MinimumLength(5));
    }
}