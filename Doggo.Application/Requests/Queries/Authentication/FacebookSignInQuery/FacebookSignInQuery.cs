namespace Doggo.Application.Requests.Queries.Authentication.FacebookSignInQuery;

using Domain.Results;
using MediatR;
using Responses.Authentication;

public record FacebookSignInQuery(string AccessToken) : IRequest<CommonResult<SignInResponse>>;