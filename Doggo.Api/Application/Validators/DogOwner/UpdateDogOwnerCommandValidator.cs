namespace Doggo.Application.Validators.DogOwner;

using FluentValidation;
using Requests.Commands.DogOwner;

public class UpdateDogOwnerCommandValidator : AbstractValidator<UpdateDogOwnerCommand>
{
    public UpdateDogOwnerCommandValidator()
    {
        When(
            x => x.Address is not null,
            () => RuleFor(x => x.Address).MinimumLength(5));

        When(x => x.District is not null,
            () => RuleFor(x => x.District).MinimumLength(5));
    }
}