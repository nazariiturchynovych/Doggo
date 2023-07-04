namespace Doggo.Application.Requests.Commands.DogOwner.UpdateDogOwnerCommand;

using FluentValidation;

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