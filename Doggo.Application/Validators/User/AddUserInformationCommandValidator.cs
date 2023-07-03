namespace Doggo.Application.Validators.User;

using System.Text.RegularExpressions;
using Domain.Constants;
using FluentValidation;
using Requests.Commands.User;

public class AddUserInformationCommandValidator : AbstractValidator<AddUserInformationCommand>
{
    public AddUserInformationCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .MinimumLength(2)
            .MaximumLength(30);

        RuleFor(x => x.LastName)
            .MinimumLength(2)
            .MaximumLength(30);

        RuleFor(x => x.PhoneNumber)
            .Matches(new Regex(ValidationConstants.PhoneRegexPattern));

        RuleFor(x => x.Age).InclusiveBetween(14, 72);
    }
}