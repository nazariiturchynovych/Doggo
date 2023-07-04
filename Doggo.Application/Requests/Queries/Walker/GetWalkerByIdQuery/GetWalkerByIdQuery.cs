namespace Doggo.Application.Requests.Queries.Walker.GetWalkerByIdQuery;

using Domain.Results;
using DTO.Walker;
using MediatR;

public record GetWalkerByIdQuery(Guid Id) : IRequest<CommonResult<GetWalkerDto>>;