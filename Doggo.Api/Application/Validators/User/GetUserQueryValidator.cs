namespace Doggo.Application.Validators.User;

using FluentValidation;
using Requests.Queries.User;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}