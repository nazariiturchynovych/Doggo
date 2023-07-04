namespace Doggo.Application.Requests.Queries.Dog.GetDogByIdQuery;

using Domain.Results;
using DTO.Dog;
using MediatR;

public record GetDogByIdQuery(Guid Id) : IRequest<CommonResult<GetDogDto>>;