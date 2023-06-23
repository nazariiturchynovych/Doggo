namespace Doggo.Application.Validators.JobRequest;

using Domain.Constants;
using FluentValidation;
using Requests.Queries.JobRequest;

public class GetPageOfJobRequestsQueryValidator : AbstractValidator<GetPageOfJobRequestsQuery>
{
    public GetPageOfJobRequestsQueryValidator()
    {
        RuleFor(x => x.Page).InclusiveBetween(9, 51);

        RuleFor(x => x.PageCount).NotEmpty();

        When(
            x => x.DescriptionSearchTerm is not null,
            () => RuleFor(x => x.DescriptionSearchTerm).NotEmpty());

        var sortConditions = new List<string>()
        {
            SortingConstants.Description,
            SortingConstants.Salary,
        };

        When(
            x => x.SortColumn is not null,
            () => RuleFor(x => x.SortColumn!.ToLower())
                .Must(x => sortConditions.Contains(x))
                .WithMessage("Please only use: " + string.Join(", ", sortConditions)));
    }
}