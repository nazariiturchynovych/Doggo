namespace Doggo.Application.Requests.Queries.Job.GetDogJobsQuery;

using FluentValidation;

public class GetDogJobsQueryValidator : AbstractValidator<GetDogJobsQuery>
{
    public GetDogJobsQueryValidator()
    {
        RuleFor(x => x.DogId).NotEmpty();
    }
}