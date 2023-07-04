namespace Doggo.Application.Requests.Queries.PossibleSchedule.GetWalkersPossibleSchedulesQuery;

using GetPossibleScheduleByIdQuery;
using FluentValidation;

public class GetPossibleScheduleByIdQueryValidator : AbstractValidator<GetPossibleScheduleByIdQuery>
{
    public GetPossibleScheduleByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}