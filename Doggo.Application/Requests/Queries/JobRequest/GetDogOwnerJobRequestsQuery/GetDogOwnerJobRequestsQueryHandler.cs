namespace Doggo.Application.Requests.Queries.JobRequest.GetDogOwnerJobRequestsQuery;

using Abstractions.Repositories;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.JobRequest;

public class GetDogOwnerJobRequestsQueryHandler : IRequestHandler<GetDogOwnerJobRequestsQuery, CommonResult<List<JobRequestResponse>>>
{
    private readonly IJobRequestRepository _jobRequestRepository;

    public GetDogOwnerJobRequestsQueryHandler(IJobRequestRepository jobRequestRepository)
    {
        _jobRequestRepository = jobRequestRepository;
    }

    public async Task<CommonResult<List<JobRequestResponse>>> Handle(
        GetDogOwnerJobRequestsQuery request,
        CancellationToken cancellationToken)
    {

        var page = await _jobRequestRepository.GetDogOwnerJobRequests(request.DogOwnerId, cancellationToken);

        return Success(page.MapJobRequestCollectionToListOJobRequestsResponse());
    }
};