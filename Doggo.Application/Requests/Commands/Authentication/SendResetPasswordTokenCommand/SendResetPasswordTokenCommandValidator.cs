namespace Doggo.Application.Requests.Commands.Authentication.SendResetPasswordTokenCommand;

using FluentValidation;

public class SendResetPasswordTokenCommandValidator : AbstractValidator<SendResetPasswordTokenCommand>
{
    public SendResetPasswordTokenCommandValidator()
    {
        RuleFor(x => x.UserEmail).EmailAddress();
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.NewPassword);
    }
}