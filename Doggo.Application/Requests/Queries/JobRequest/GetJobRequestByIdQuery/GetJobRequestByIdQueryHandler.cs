namespace Doggo.Application.Requests.Queries.JobRequest.GetJobRequestByIdQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.JobRequest;

public class GetJobRequestByIdQueryHandler : IRequestHandler<GetJobRequestByIdQuery, CommonResult<JobRequestResponse>>
{
    private readonly IJobRequestRepository _jobRequestRepository;
    private readonly ICacheService _cacheService;

    public GetJobRequestByIdQueryHandler(ICacheService cacheService, IJobRequestRepository jobRequestRepository)
    {
        _cacheService = cacheService;
        _jobRequestRepository = jobRequestRepository;
    }

    public async Task<CommonResult<JobRequestResponse>> Handle(
        GetJobRequestByIdQuery request,
        CancellationToken cancellationToken)
    {
        var cachedEntity = await _cacheService.GetData<JobRequestResponse>(
            CacheKeys.JobRequest + request.Id,
            cancellationToken);

        if (cachedEntity is null)
        {
            var jobRequest = await _jobRequestRepository.GetAsync(request.Id, cancellationToken);

            if (jobRequest is null)
                return Failure<JobRequestResponse>(CommonErrors.EntityDoesNotExist);

            var entityDto = jobRequest.MapJobRequestToJobRequestResponse();

            cachedEntity = entityDto;

            await _cacheService.SetData(CacheKeys.DogOwner + jobRequest.Id, entityDto, cancellationToken);
        }

        return Success(cachedEntity);
    }
}