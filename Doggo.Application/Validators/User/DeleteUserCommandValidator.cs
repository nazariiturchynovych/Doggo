namespace Doggo.Application.Validators.User;

using FluentValidation;
using Requests.Commands.User;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}