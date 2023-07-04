namespace Doggo.Application.Requests.Commands.Authentication.ChangePasswordCommand;

using FluentValidation;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.NewPassword).NotEqual(x => x.CurrentPassword);
    }
}