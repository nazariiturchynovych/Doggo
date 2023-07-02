namespace Doggo.Application.Validators.Job;

using Doggo.Application.Requests.Queries.Job;
using FluentValidation;

public class GetDogOwnerJobsQueryValidator : AbstractValidator<GetDogOwnerJobsQuery>
{
    public GetDogOwnerJobsQueryValidator()
    {
        RuleFor(x => x.DogOwnerId).NotEmpty();
    }
}