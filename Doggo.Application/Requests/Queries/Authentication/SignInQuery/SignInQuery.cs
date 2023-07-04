namespace Doggo.Application.Requests.Queries.Authentication.SignInQuery;

using Domain.Results;
using DTO.Authentication;
using MediatR;

public record SignInQuery
(
    string Email,
    string Password
) : IRequest<CommonResult<SignInDto>>;