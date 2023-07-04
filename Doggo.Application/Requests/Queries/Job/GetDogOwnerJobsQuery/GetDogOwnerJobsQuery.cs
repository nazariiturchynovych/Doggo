namespace Doggo.Application.Requests.Queries.Job.GetDogOwnerJobsQuery;

using Domain.Results;
using DTO;
using DTO.Job;
using MediatR;

public record GetDogOwnerJobsQuery(Guid DogOwnerId) : IRequest<CommonResult<PageOfTDataDto<GetJobDto>>>;