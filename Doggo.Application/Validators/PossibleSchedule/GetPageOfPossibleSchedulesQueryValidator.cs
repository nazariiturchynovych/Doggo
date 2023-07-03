namespace Doggo.Application.Validators.PossibleSchedule;

using FluentValidation;
using Requests.Queries.Walker.PossibleSchedule;

public class GetPageOfPossibleSchedulesQueryValidator : AbstractValidator<GetPageOfPossibleSchedulesQuery>
{
    public GetPageOfPossibleSchedulesQueryValidator()
    {
        RuleFor(x => x.Page).InclusiveBetween(5, 20);

        RuleFor(x => x.PageCount).NotEmpty();

    }
}