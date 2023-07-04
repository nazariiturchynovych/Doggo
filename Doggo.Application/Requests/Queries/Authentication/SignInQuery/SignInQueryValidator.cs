namespace Doggo.Application.Requests.Queries.Authentication.SignInQuery;

using FluentValidation;

public class SignInQueryValidator : AbstractValidator<SignInQuery>
{
    public SignInQueryValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).MinimumLength(3).MaximumLength(30);
    }
}