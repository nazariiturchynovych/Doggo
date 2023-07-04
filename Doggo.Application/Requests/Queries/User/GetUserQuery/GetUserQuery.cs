namespace Doggo.Application.Requests.Queries.User.GetUserQuery;

using Domain.Results;
using DTO.User;
using MediatR;

public record GetUserQuery(Guid UserId) : IRequest<CommonResult<GetUserDto>>;