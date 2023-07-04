namespace Doggo.Application.Requests.Commands.User.DeleteUserCommand;

using FluentValidation;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}