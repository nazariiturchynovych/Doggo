namespace Doggo.Application.Validators.Authentication;

using FluentValidation;
using Requests.Commands.Authentication;

public class SendEmailConfirmationTokenCommandValidator : AbstractValidator<SendEmailConfirmationTokenCommand>
{
    public SendEmailConfirmationTokenCommandValidator()
    {
        RuleFor(x => x.UserEmail).EmailAddress();
    }
}