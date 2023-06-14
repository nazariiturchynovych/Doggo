namespace Doggo.Application.Validators.Authentication;

using FluentValidation;
using Requests.Queries.Authentication;

public class SignInQueryValidator : AbstractValidator<SignInQuery>
{
    public SignInQueryValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).MinimumLength(3);
    }
}