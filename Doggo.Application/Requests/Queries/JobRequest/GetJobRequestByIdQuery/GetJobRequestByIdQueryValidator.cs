namespace Doggo.Application.Requests.Queries.JobRequest.GetJobRequestByIdQuery;

using FluentValidation;

public class GetJobRequestByIdQueryValidator : AbstractValidator<GetJobRequestByIdQuery>
{
    public GetJobRequestByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}