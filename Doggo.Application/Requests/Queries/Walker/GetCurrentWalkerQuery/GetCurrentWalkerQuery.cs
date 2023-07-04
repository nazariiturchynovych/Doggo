namespace Doggo.Application.Requests.Queries.Walker.GetCurrentWalkerQuery;

using Domain.Results;
using DTO.Walker;
using MediatR;

public record GetCurrentWalkerQuery(Guid UserId) : IRequest<CommonResult<GetWalkerDto>>;