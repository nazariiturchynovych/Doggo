namespace Doggo.Application.Requests.Queries.DogOwner.GetDogOwnerByIdQuery;

using FluentValidation;

public class GetDogOwnerByIdQueryValidator : AbstractValidator<GetDogOwnerByIdQuery>
{
    public GetDogOwnerByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}