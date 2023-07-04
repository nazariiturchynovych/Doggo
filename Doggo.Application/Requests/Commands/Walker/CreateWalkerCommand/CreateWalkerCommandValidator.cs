namespace Doggo.Application.Requests.Commands.Walker.CreateWalkerCommand;

using FluentValidation;

public class CreateWalkerCommandValidator : AbstractValidator<CreateWalkerCommand>
{
    public CreateWalkerCommandValidator()
    {
        RuleFor(x => x.Skills).MinimumLength(5);
        RuleFor(x => x.About).MinimumLength(5);
    }
}