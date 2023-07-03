namespace Doggo.Application.Validators.Walker;

using FluentValidation;
using Requests.Queries.Walker;

public class GetWalkerByIdQueryValidator : AbstractValidator<GetWalkerByIdQuery>
{
    public GetWalkerByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}