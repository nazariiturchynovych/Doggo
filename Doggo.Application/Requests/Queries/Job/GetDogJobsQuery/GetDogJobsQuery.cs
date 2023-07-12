namespace Doggo.Application.Requests.Queries.Job.GetDogJobsQuery;

using Domain.Results;
using MediatR;
using Responses.Job;

public record GetDogJobsQuery(Guid DogId) : IRequest<CommonResult<List<JobResponse>>>;