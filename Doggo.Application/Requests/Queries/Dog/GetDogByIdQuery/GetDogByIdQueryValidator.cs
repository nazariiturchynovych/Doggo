namespace Doggo.Application.Requests.Queries.Dog.GetDogByIdQuery;

using FluentValidation;

public class GetDogByIdQueryValidator : AbstractValidator<GetDogByIdQuery>
{
    public GetDogByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}