namespace Doggo.Application.Requests.Commands.Authentication.SendEmailConfirmationTokenCommand;

using FluentValidation;

public class SendEmailConfirmationTokenCommandValidator : AbstractValidator<SendEmailConfirmationTokenCommand>
{
    public SendEmailConfirmationTokenCommandValidator()
    {
        RuleFor(x => x.UserEmail).EmailAddress();
    }
}