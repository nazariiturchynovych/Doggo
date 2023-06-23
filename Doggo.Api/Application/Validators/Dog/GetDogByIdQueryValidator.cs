namespace Doggo.Application.Validators.Dog;

using FluentValidation;
using Requests.Queries.Dog;

public class GetDogByIdQueryValidator : AbstractValidator<GetDogByIdQuery>
{
    public GetDogByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}