namespace Doggo.Application.Validators.Authentication;

using FluentValidation;
using Requests.Commands.Authentication;

public class ConfirmEmailAndSetDefaultRoleCommandValidator : AbstractValidator<ConfirmEmailAndSetDefaultRoleCommand>
{
    public ConfirmEmailAndSetDefaultRoleCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}