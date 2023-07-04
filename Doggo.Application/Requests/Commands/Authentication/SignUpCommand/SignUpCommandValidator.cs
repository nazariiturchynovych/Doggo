namespace Doggo.Application.Requests.Commands.Authentication.SignUpCommand;

using FluentValidation;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
    }
}