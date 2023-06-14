namespace Doggo.Application.Validators.User;

using FluentValidation;
using Requests.Commands.User;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Age)
            .GreaterThan(14)
            .LessThan(72);

        RuleFor(x => x.FirstName)
            .MinimumLength(1)
            .NotEmpty()
            .MaximumLength(30)
            .Matches("/^[a-z ,.'-]+$/i");

    }
}