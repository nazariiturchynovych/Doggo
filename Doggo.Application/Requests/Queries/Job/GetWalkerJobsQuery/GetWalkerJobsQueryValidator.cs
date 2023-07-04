namespace Doggo.Application.Requests.Queries.Job.GetWalkerJobsQuery;

using FluentValidation;

public class GetWalkerJobsQueryValidator : AbstractValidator<GetWalkerJobsQuery>
{
    public GetWalkerJobsQueryValidator()
    {
        RuleFor(x => x.WalkerId).NotEmpty();
    }
}