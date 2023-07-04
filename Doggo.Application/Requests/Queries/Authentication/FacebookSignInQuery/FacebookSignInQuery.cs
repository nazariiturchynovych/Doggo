namespace Doggo.Application.Requests.Queries.Authentication.FacebookSignInQuery;

using Domain.Results;
using DTO.Authentication;
using MediatR;

public record FacebookSignInQuery(string AccessToken) : IRequest<CommonResult<SignInDto>>;