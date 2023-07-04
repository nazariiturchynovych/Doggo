namespace Doggo.Application.Requests.Queries.Walker.GetWalkerByIdQuery;

using FluentValidation;

public class GetWalkerByIdQueryValidator : AbstractValidator<GetWalkerByIdQuery>
{
    public GetWalkerByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}