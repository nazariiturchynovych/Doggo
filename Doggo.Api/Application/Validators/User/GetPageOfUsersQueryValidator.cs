namespace Doggo.Application.Validators.User;

using FluentValidation;
using Requests.Queries.User;

public class GetPageOfUsersQueryValidator : AbstractValidator<GetPageOfUsersQuery>
{
    public GetPageOfUsersQueryValidator()
    {
        RuleFor(x => x.Count).InclusiveBetween(9, 51);
        RuleFor(x => x.PageCount).NotEmpty();
    }
}