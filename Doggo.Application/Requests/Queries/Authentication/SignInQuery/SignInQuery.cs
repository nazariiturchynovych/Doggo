namespace Doggo.Application.Requests.Queries.Authentication.SignInQuery;

using Domain.Results;
using MediatR;
using Responses.Authentication;

public record SignInQuery
(
    string Email,
    string Password
) : IRequest<CommonResult<SignInResponse>>;