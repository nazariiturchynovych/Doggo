namespace Doggo.Application.Requests.Queries.Walker.GetCurrentWalkerQuery;

using Domain.Results;
using MediatR;
using Responses.Walker;

public record GetCurrentWalkerQuery(Guid UserId) : IRequest<CommonResult<WalkerResponse>>;