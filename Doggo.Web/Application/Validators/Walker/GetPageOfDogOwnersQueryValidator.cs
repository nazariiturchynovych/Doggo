namespace Doggo.Application.Validators.Walker;

using Domain.Constants;
using FluentValidation;
using Requests.Queries.Walker;

public class GetPageOfWalkersQueryValidator : AbstractValidator<GetPageOfWalkersQuery>
{
    public GetPageOfWalkersQueryValidator()
    {
        RuleFor(x => x.Page).InclusiveBetween(9, 51);

        RuleFor(x => x.PageCount).NotEmpty();

        When(
            x => x.NameSearchTerm is not null,
            () => RuleFor(x => x.NameSearchTerm).NotEmpty());

        var sortConditions = new List<string>()
        {
            SortingConstants.FirstName,
            SortingConstants.Lastname,
            SortingConstants.Age,
            SortingConstants.About,
            SortingConstants.Skills
        };

        When(
            x => x.SortColumn is not null,
            () => RuleFor(x => x.SortColumn!.ToLower())
                .Must(x => sortConditions.Contains(x))
                .WithMessage("Please only use: " + string.Join(", ", sortConditions)));
    }
}