namespace Doggo.Application.Requests.Commands.Dog.UpdateDogCommand;

using FluentValidation;

public class UpdateDogCommandValidator : AbstractValidator<UpdateDogCommand>
{
    public UpdateDogCommandValidator()
    {

        RuleFor(x => x.DogId).NotEmpty();

        When(
            x => x.Age is not null,
            () => RuleFor(x => x.Age).GreaterThan(0).LessThan(30));

        When(
            x => x.Name is not null,
            () => RuleFor(x => x.Name).MinimumLength(1).MaximumLength(20));

        When(
            x => x.Description is not null,
            () => RuleFor(x => x.Description).NotEmpty().MaximumLength(150));

        When(
            x => x.Weight is not null,
            () => RuleFor(x => x.Weight)
                .NotEmpty()
                .GreaterThan(0)
                .LessThan(100));
    }
}