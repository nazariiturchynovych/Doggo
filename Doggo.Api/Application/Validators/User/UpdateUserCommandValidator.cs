namespace Doggo.Application.Validators.User;

using System.Text.RegularExpressions;
using Domain.Constants;
using FluentValidation;
using Requests.Commands.User;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        When(
            x => x.Age is not null,
            () => RuleFor(x => x.Age).InclusiveBetween(14, 72));

        When(
            x => x.FirstName is not null,
            () => RuleFor(x => x.FirstName)
                .MinimumLength(2)
                .MaximumLength(30));

        When(x => x.LastName is not null,
            () => RuleFor(x => x.LastName)
                .MinimumLength(2)
                .MaximumLength(30));

        When(x => x.PhoneNumber is not null,
            () => RuleFor(x => x.PhoneNumber)
                .Matches(new Regex(ValidationConstants.PhoneRegexPattern)));
    }
}