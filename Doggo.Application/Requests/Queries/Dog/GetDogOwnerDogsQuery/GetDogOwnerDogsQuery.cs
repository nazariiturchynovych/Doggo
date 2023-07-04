namespace Doggo.Application.Requests.Queries.Dog.GetDogOwnerDogsQuery;

using Domain.Results;
using DTO;
using DTO.Dog;
using MediatR;

public record GetDogOwnerDogsQuery(Guid DogOwnerId) : IRequest<CommonResult<PageOfTDataDto<GetDogDto>>>;