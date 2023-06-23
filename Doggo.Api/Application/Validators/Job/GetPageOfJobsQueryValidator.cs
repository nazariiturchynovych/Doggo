namespace Doggo.Application.Validators.Job;

using Domain.Constants;
using FluentValidation;
using Requests.Queries.Job;

public class GetPageOfJobsQueryValidator : AbstractValidator<GetPageOfJobsQuery>
{
    public GetPageOfJobsQueryValidator()
    {
        RuleFor(x => x.Page).InclusiveBetween(9, 51);

        RuleFor(x => x.PageCount).NotEmpty();

        When(
            x => x.CommentSearchTerm is not null,
            () => RuleFor(x => x.CommentSearchTerm).NotEmpty());

        var sortConditions = new List<string>()
        {
            SortingConstants.Comment,
            SortingConstants.Salary,
        };

        When(
            x => x.SortColumn is not null,
            () => RuleFor(x => x.SortColumn!.ToLower())
                .Must(x => sortConditions.Contains(x))
                .WithMessage("Please only use: " + string.Join(", ", sortConditions)));
    }
}