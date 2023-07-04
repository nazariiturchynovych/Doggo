namespace Doggo.Application.Requests.Queries.PossibleSchedule.GetPageOfPossibleSchedulesQuery;

using FluentValidation;

public class GetPageOfPossibleSchedulesQueryValidator : AbstractValidator<GetPageOfPossibleSchedulesQuery>
{
    public GetPageOfPossibleSchedulesQueryValidator()
    {
        RuleFor(x => x.Page).InclusiveBetween(5, 20);

        RuleFor(x => x.PageCount).NotEmpty();

    }
}