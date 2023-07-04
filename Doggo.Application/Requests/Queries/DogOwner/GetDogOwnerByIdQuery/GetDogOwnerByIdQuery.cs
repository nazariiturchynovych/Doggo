namespace Doggo.Application.Requests.Queries.DogOwner.GetDogOwnerByIdQuery;

using Domain.Results;
using DTO.DogOwner;
using MediatR;

public record GetDogOwnerByIdQuery(Guid Id) : IRequest<CommonResult<GetDogOwnerDto>>;