namespace Doggo.Application.Validators.PossibleSchedule;

using FluentValidation;
using Requests.Queries.Walker.PossibleSchedule;

public class GetPossibleScheduleByIdQueryValidator : AbstractValidator<GetPossibleScheduleByIdQuery>
{
    public GetPossibleScheduleByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}