namespace Doggo.Application.Requests.Commands.DogOwner.DeleteDogOwnerCommand;

using FluentValidation;

public class DeleteDogOwnerCommandValidator : AbstractValidator<DeleteDogOwnerCommand>
{
    public DeleteDogOwnerCommandValidator()
    {
        RuleFor(x => x.DogOwnerId).NotEmpty();
    }
}