namespace Doggo.Application.Requests.Queries.Job.GetJobByIdQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.Job;

public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, CommonResult<JobResponse>>
{
    private readonly ICacheService _cacheService;
    private readonly IJobRepository _jobRepository;


    public GetJobByIdQueryHandler(ICacheService cacheService, IJobRepository jobRepository)
    {
        _cacheService = cacheService;
        _jobRepository = jobRepository;
    }

    public async Task<CommonResult<JobResponse>> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
    {
        var cachedEntity = await _cacheService.GetData<JobResponse>(CacheKeys.Job + request.Id, cancellationToken);

        if (cachedEntity is null)
        {
            var job = await _jobRepository.GetAsync(request.Id, cancellationToken);

            if (job is null)
                return Failure<JobResponse>(CommonErrors.EntityDoesNotExist);

            var entityDto = job.MapJobToJobResponse();

            cachedEntity = entityDto;

            await _cacheService.SetData(CacheKeys.DogOwner + job.Id, entityDto, cancellationToken);
        }

        return Success(cachedEntity);
    }
}