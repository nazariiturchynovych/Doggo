namespace Doggo.Application.Requests.Queries.Authentication.GoogleSignInQuery;

using Domain.Results;
using DTO.Authentication;
using MediatR;

public record GoogleSignInQuery(string Credential) : IRequest<CommonResult<SignInDto>>;