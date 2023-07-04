namespace Doggo.Application.Requests.Queries.Job.GetWalkerJobsQuery;

using Domain.Results;
using DTO;
using DTO.Job;
using MediatR;

public record GetWalkerJobsQuery(Guid WalkerId) : IRequest<CommonResult<PageOfTDataDto<GetJobDto>>>;