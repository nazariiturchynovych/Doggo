namespace Doggo.Application.Requests.Commands.User.AddUserPersonalIdentifierCommand;

using FluentValidation;

public class AddUserPersonalIdentifierValidator : AbstractValidator<AddUserPersonalIdentifierCommand>
{
    public AddUserPersonalIdentifierValidator()
    {
        RuleFor(x => x.IdentifierType).IsInEnum();
    }
}