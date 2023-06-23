namespace Doggo.Application.Validators.Job;

using Doggo.Application.Requests.Queries.Job;
using FluentValidation;

public class GetWalkerJobsQueryValidator : AbstractValidator<GetWalkerJobsQuery>
{
    public GetWalkerJobsQueryValidator()
    {
        RuleFor(x => x.WalkerId).NotEmpty();
    }
}