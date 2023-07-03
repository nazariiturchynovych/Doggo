namespace Doggo.Application.Validators.Dog;

using FluentValidation;
using Requests.Commands.Dog;

public class DeleteDogCommandValidator : AbstractValidator<DeleteDogCommand>
{
    public DeleteDogCommandValidator()
    {
        RuleFor(x => x.DogId).NotEmpty();
    }
}