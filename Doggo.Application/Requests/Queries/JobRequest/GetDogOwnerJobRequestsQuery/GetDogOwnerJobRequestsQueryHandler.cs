namespace Doggo.Application.Requests.Queries.JobRequest.GetDogOwnerJobRequestsQuery;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.JobRequest;
using Mappers;
using MediatR;

public class GetDogOwnerJobRequestsQueryHandler : IRequestHandler<GetDogOwnerJobRequestsQuery, CommonResult<PageOfTDataDto<GetJobRequestDto>>>
{
    private readonly IJobRequestRepository _jobRequestRepository;

    public GetDogOwnerJobRequestsQueryHandler(IJobRequestRepository jobRequestRepository)
    {
        _jobRequestRepository = jobRequestRepository;
    }

    public async Task<CommonResult<PageOfTDataDto<GetJobRequestDto>>> Handle(
        GetDogOwnerJobRequestsQuery request,
        CancellationToken cancellationToken)
    {

        var page = await _jobRequestRepository.GetDogOwnerJobRequests(request.DogOwnerId, cancellationToken);

        return Success(page.MapJobRequestCollectionToPageOJobRequestsDto());
    }
};