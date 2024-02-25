namespace Doggo.Application.Requests.Queries.JobRequest.GetPageOfJobRequestsQuery;

using Domain.Constants;
using FluentValidation;

public class GetPageOfJobRequestsQueryValidator : AbstractValidator<GetPageOfJobRequestsQuery>
{
    public GetPageOfJobRequestsQueryValidator()
    {
        RuleFor(x => x.Page).NotEmpty();

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