namespace Doggo.Application.Requests.Queries.Job.GetDogOwnerJobsQuery;

using Domain.Results;
using MediatR;
using Responses.Job;

public record GetDogOwnerJobsQuery(Guid DogOwnerId) : IRequest<CommonResult<List<JobResponse>>>;