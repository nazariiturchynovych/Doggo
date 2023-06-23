namespace Doggo.Application.Validators.Dog;

using FluentValidation;
using Requests.Commands.Dog;

public class CreateDogCommandValidator : AbstractValidator<CreateDogCommand>
{
    public CreateDogCommandValidator()
    {
        RuleFor(x => x.DogOwnerId).NotEmpty();

        RuleFor(x => x.Age).GreaterThan(0).LessThan(30);

        RuleFor(x => x.Name).MinimumLength(1).MaximumLength(20);

        RuleFor(x => x.Description).NotEmpty().MaximumLength(150);

        When(x => x.Weight is not null,
            () => RuleFor(x => x.Weight)
                .NotEmpty()
                .GreaterThan(0)
                .LessThan(100));
    }
}