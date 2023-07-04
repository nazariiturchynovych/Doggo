namespace Doggo.Application.Requests.Queries.JobRequest.GetDogOwnerJobRequestsQuery;

using Domain.Results;
using DTO;
using DTO.JobRequest;
using MediatR;

public record GetDogOwnerJobRequestsQuery(Guid DogOwnerId) : IRequest<CommonResult<PageOfTDataDto<GetJobRequestDto>>>;