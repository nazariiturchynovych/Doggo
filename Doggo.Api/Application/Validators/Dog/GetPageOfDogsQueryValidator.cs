namespace Doggo.Application.Validators.Dog;

using Domain.Constants;
using FluentValidation;
using Requests.Queries.Dog;

public class GetPageOfDogsQueryValidator : AbstractValidator<GetPageOfDogsQuery>
{
    public GetPageOfDogsQueryValidator()
    {
        RuleFor(x => x.Page).InclusiveBetween(5, 51);
        RuleFor(x => x.PageCount).NotEmpty();

        When(
            x => x.NameSearchTerm is not null,
            () => RuleFor(x => x.NameSearchTerm).NotEmpty());

        When(
            x => x.DescriptionSearchTerm is not null,
            () => RuleFor(x => x.DescriptionSearchTerm).NotEmpty());

        var sortConditions = new List<string>()
        {
            SortingConstants.Name,
            SortingConstants.Description,
            SortingConstants.Age,
            SortingConstants.Weight,
        };

        When(
            x => x.SortColumn is not null,
            () => RuleFor(x => x.SortColumn!.ToLower())
                .Must(x => sortConditions.Contains(x))
                .WithMessage("Please only use: " + string.Join(", ", sortConditions)));
    }
}