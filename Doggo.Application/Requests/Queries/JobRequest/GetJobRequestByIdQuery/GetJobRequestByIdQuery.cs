namespace Doggo.Application.Requests.Queries.JobRequest.GetJobRequestByIdQuery;

using Domain.Results;
using MediatR;
using Responses.JobRequest;

public record GetJobRequestByIdQuery(Guid Id) : IRequest<CommonResult<JobRequestResponse>>;