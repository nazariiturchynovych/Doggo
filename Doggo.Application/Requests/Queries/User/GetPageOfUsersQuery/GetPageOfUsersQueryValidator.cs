namespace Doggo.Application.Requests.Queries.User.GetPageOfUsersQuery;

using FluentValidation;

public class GetPageOfUsersQueryValidator : AbstractValidator<GetPageOfUsersQuery>
{
    public GetPageOfUsersQueryValidator()
    {
        RuleFor(x => x.Page).NotEmpty();
        RuleFor(x => x.PageCount).NotEmpty();
    }
}