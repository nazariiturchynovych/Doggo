namespace Doggo.Application.Requests.Queries.User.GetUserQuery;

using Domain.Results;
using MediatR;
using Responses.User;

public record GetUserQuery(Guid Id) : IRequest<CommonResult<UserResponse>>;