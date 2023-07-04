namespace Doggo.Application.Requests.Queries.Job.GetJobByIdQuery;

using FluentValidation;

public class GetJobByIdQueryValidator : AbstractValidator<GetJobByIdQuery>
{
    public GetJobByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}