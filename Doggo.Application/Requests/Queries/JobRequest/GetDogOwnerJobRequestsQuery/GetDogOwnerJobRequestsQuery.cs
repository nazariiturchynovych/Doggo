namespace Doggo.Application.Requests.Queries.JobRequest.GetDogOwnerJobRequestsQuery;

using Domain.Results;
using MediatR;
using Responses.JobRequest;

public record GetDogOwnerJobRequestsQuery(Guid DogOwnerId) : IRequest<CommonResult<List<JobRequestResponse>>>;