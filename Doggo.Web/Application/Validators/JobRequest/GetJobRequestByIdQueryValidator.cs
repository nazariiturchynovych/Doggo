namespace Doggo.Application.Validators.JobRequest;

using FluentValidation;
using Requests.Queries.JobRequest;

public class GetJobRequestByIdQueryValidator : AbstractValidator<GetJobRequestByIdQuery>
{
    public GetJobRequestByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}