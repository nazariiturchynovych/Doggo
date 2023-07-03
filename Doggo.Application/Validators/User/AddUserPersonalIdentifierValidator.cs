namespace Doggo.Application.Validators.User;

using FluentValidation;
using Requests.Commands.User;

public class AddUserPersonalIdentifierValidator : AbstractValidator<AddUserPersonalIdentifierCommand>
{
    public AddUserPersonalIdentifierValidator()
    {
        RuleFor(x => x.IdentifierType).IsInEnum();
    }
}