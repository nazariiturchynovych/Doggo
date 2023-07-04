namespace Doggo.Application.Requests.Commands.DogOwner.CreateDogOwnerCommand;

using FluentValidation;

public class CreateDogOwnerCommandValidator : AbstractValidator<CreateDogOwnerCommand>
{
    public CreateDogOwnerCommandValidator()
    {
        RuleFor(x => x.Address).MinimumLength(5);
        RuleFor(x => x.District).MinimumLength(5);
    }
}