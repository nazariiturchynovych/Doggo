namespace Doggo.Application.Requests.Commands.Walker.UpdateWalkerCommand;

using FluentValidation;

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