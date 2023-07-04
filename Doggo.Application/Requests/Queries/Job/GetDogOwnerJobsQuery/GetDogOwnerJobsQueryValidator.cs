namespace Doggo.Application.Requests.Queries.Job.GetDogOwnerJobsQuery;

using FluentValidation;

public class GetDogOwnerJobsQueryValidator : AbstractValidator<GetDogOwnerJobsQuery>
{
    public GetDogOwnerJobsQueryValidator()
    {
        RuleFor(x => x.DogOwnerId).NotEmpty();
    }
}