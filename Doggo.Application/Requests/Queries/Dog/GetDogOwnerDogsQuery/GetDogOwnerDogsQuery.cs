namespace Doggo.Application.Requests.Queries.Dog.GetDogOwnerDogsQuery;

using Domain.Results;
using MediatR;
using Responses.Dog;

public record GetDogOwnerDogsQuery(Guid DogOwnerId) : IRequest<CommonResult<List<DogResponse>>>;