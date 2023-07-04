namespace Doggo.Application.Requests.Commands.Dog.DeleteDogCommand;

using FluentValidation;

public class DeleteDogCommandValidator : AbstractValidator<DeleteDogCommand>
{
    public DeleteDogCommandValidator()
    {
        RuleFor(x => x.DogId).NotEmpty();
    }
}