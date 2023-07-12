namespace Doggo.Application.Requests.Queries.DogOwner.GetCurrentDogOwnerQuery;

using Domain.Results;
using MediatR;
using Responses.DogOwner;

public record GetCurrentDogOwnerQuery(Guid UserId) : IRequest<CommonResult<DogOwnerResponse>>;