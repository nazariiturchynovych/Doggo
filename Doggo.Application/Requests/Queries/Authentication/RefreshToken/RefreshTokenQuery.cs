namespace Doggo.Application.Requests.Queries.Authentication.RefreshToken;

using Domain.Results;
using MediatR;
using Responses.Authentication;

public record RefreshTokenQuery
(
    string RefreshToken,
    string Token
) : IRequest<CommonResult<SignInResponse>>;