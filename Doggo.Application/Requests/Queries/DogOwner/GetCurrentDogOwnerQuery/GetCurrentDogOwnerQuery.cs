namespace Doggo.Application.Requests.Queries.DogOwner.GetCurrentDogOwnerQuery;

using Domain.Results;
using DTO.DogOwner;
using MediatR;

public record GetCurrentDogOwnerQuery(Guid UserId) : IRequest<CommonResult<GetDogOwnerDto>>;