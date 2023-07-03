namespace Doggo.Application.Validators.Job;

using FluentValidation;
using Requests.Queries.Job;

public class GetDogJobsQueryValidator : AbstractValidator<GetDogJobsQuery>
{
    public GetDogJobsQueryValidator()
    {
        RuleFor(x => x.DogId).NotEmpty();
    }
}