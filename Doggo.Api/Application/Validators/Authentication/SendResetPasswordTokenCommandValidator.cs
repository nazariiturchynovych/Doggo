namespace Doggo.Application.Validators.Authentication;

using FluentValidation;
using Requests.Commands.Authentication;

public class SendResetPasswordTokenCommandValidator : AbstractValidator<SendResetPasswordTokenCommand>
{
    public SendResetPasswordTokenCommandValidator()
    {
        RuleFor(x => x.UserEmail).EmailAddress();
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.NewPassword);
    }
}