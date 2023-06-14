namespace Doggo.Application.Validators.Authentication;

using FluentValidation;
using Requests.Commands.Authentication;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
    }
}