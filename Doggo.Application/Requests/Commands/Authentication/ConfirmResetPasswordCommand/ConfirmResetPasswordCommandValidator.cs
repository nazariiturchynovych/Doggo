namespace Doggo.Application.Requests.Commands.Authentication.ConfirmResetPasswordCommand;

using FluentValidation;

public class ConfirmResetPasswordCommandValidator : AbstractValidator<ConfirmResetPasswordCommand>
{
    public ConfirmResetPasswordCommandValidator()
    {
        RuleFor(x => x.NewPassword).MinimumLength(3);
    }
}