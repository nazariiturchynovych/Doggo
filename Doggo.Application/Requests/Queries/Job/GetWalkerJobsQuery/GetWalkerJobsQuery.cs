namespace Doggo.Application.Requests.Queries.Job.GetWalkerJobsQuery;

using Domain.Results;
using MediatR;
using Responses.Job;

public record GetWalkerJobsQuery(Guid WalkerId) : IRequest<CommonResult<List<JobResponse>>>;