namespace Doggo.Application.Validators.Authentication;

using FluentValidation;
using Requests.Commands.Authentication;

public class ConfirmResetPasswordCommandValidator : AbstractValidator<ConfirmResetPasswordCommand>
{
    public ConfirmResetPasswordCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.NewPassword).MinimumLength(3);
    }
}