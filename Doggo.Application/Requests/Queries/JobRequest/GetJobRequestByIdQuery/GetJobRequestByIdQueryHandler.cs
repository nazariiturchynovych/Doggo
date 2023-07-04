namespace Doggo.Application.Requests.Queries.JobRequest.GetJobRequestByIdQuery;

using Abstractions.Persistence.Read;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO.JobRequest;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public class GetJobRequestByIdQueryHandler : IRequestHandler<GetJobRequestByIdQuery, CommonResult<GetJobRequestDto>>
{
    private readonly IJobRequestRepository _jobRequestRepository;
    private readonly ICacheService _cacheService;

    public GetJobRequestByIdQueryHandler(ICacheService cacheService, IJobRequestRepository jobRequestRepository)
    {
        _cacheService = cacheService;
        _jobRequestRepository = jobRequestRepository;
    }

    public async Task<CommonResult<GetJobRequestDto>> Handle(
        GetJobRequestByIdQuery request,
        CancellationToken cancellationToken)
    {
        var cachedEntity = await _cacheService.GetData<GetJobRequestDto>(
            CacheKeys.JobRequest + request.Id,
            cancellationToken);

        if (cachedEntity is null)
        {
            var jobRequest = await _jobRequestRepository.GetAsync(request.Id, cancellationToken);

            if (jobRequest is null)
                return Failure<GetJobRequestDto>(CommonErrors.EntityDoesNotExist);

            var entityDto = jobRequest.MapJobRequestToGetJobRequestDto();

            cachedEntity = entityDto;

            await _cacheService.SetData(CacheKeys.DogOwner + jobRequest.Id, entityDto, cancellationToken);
        }

        return Success(cachedEntity);
    }
}