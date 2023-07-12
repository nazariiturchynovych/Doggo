namespace Doggo.Application.Requests.Queries.Job.GetJobByIdQuery;

using Domain.Results;
using MediatR;
using Responses.Job;

public record GetJobByIdQuery(Guid Id) : IRequest<CommonResult<JobResponse>>;