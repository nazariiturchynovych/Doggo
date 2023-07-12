namespace Doggo.Application.Requests.Queries.Authentication.GoogleSignInQuery;

using Domain.Results;
using MediatR;
using Responses.Authentication;

public record GoogleSignInQuery(string Credential) : IRequest<CommonResult<SignInResponse>>;