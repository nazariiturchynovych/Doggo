namespace Doggo.Application.Validators.DogOwner;

using FluentValidation;
using Requests.Commands.DogOwner;

public class DeleteDogOwnerCommandValidator : AbstractValidator<DeleteDogOwnerCommand>
{
    public DeleteDogOwnerCommandValidator()
    {
        RuleFor(x => x.DogOwnerId).NotEmpty();
    }
}