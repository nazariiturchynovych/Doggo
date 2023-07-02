namespace Doggo.Application.Validators.Job;

using FluentValidation;
using Requests.Queries.Job;

public class GetJobByIdQueryValidator : AbstractValidator<GetJobByIdQuery>
{
    public GetJobByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}