namespace Doggo.Application.Validators.DogOwner;

using FluentValidation;
using Requests.Queries.DogOwner;

public class GetDogOwnerByIdQueryValidator : AbstractValidator<GetDogOwnerByIdQuery>
{
    public GetDogOwnerByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}