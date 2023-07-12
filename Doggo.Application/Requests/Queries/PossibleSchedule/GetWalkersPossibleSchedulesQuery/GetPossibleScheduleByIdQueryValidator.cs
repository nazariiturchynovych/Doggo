namespace Doggo.Application.Requests.Queries.PossibleSchedule.GetWalkersPossibleSchedulesQuery;

using FluentValidation;
using GetPossibleScheduleByIdQuery;

public class GetPossibleScheduleByIdQueryValidator : AbstractValidator<GetPossibleScheduleByIdQuery>
{
    public GetPossibleScheduleByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}