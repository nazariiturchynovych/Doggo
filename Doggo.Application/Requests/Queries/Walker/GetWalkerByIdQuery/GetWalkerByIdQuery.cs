namespace Doggo.Application.Requests.Queries.Walker.GetWalkerByIdQuery;

using Domain.Results;
using MediatR;
using Responses.Walker;

public record GetWalkerByIdQuery(Guid Id) : IRequest<CommonResult<WalkerResponse>>;