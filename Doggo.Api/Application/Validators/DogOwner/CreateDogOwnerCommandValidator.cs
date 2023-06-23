namespace Doggo.Application.Validators.DogOwner;

using FluentValidation;
using Requests.Commands.DogOwner;

public class CreateDogOwnerCommandValidator : AbstractValidator<CreateDogOwnerCommand>
{
    public CreateDogOwnerCommandValidator()
    {
        RuleFor(x => x.Address).MinimumLength(5);
        RuleFor(x => x.District).MinimumLength(5);
    }
}