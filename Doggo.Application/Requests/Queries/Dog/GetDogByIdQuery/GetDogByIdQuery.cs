namespace Doggo.Application.Requests.Queries.Dog.GetDogByIdQuery;

using Domain.Results;
using MediatR;
using Responses.Dog;

public record GetDogByIdQuery(Guid Id) : IRequest<CommonResult<DogResponse>>;