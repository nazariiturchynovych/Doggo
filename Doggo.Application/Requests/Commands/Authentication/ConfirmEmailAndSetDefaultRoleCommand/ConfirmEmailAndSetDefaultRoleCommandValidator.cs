namespace Doggo.Application.Requests.Commands.Authentication.ConfirmEmailAndSetDefaultRoleCommand;

using FluentValidation;

public class ConfirmEmailAndSetDefaultRoleCommandValidator : AbstractValidator<ConfirmEmailAndSetDefaultRoleCommand>
{
    public ConfirmEmailAndSetDefaultRoleCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}