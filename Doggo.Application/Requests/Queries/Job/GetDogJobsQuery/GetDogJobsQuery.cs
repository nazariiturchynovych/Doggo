namespace Doggo.Application.Requests.Queries.Job.GetDogJobsQuery;

using Domain.Results;
using DTO;
using DTO.Job;
using MediatR;

public record GetDogJobsQuery(Guid DogId) : IRequest<CommonResult<PageOfTDataDto<GetJobDto>>>;