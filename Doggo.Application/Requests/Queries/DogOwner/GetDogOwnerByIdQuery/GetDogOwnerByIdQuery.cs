namespace Doggo.Application.Requests.Queries.DogOwner.GetDogOwnerByIdQuery;

using Domain.Results;
using MediatR;
using Responses.DogOwner;

public record GetDogOwnerByIdQuery(Guid Id) : IRequest<CommonResult<DogOwnerResponse>>;