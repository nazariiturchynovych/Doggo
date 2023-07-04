namespace Doggo.Application.Requests.Queries.Job.GetJobByIdQuery;

using Abstractions.Persistence.Read;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO.Job;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, CommonResult<GetJobDto>>
{
    private readonly ICacheService _cacheService;
    private readonly IJobRepository _jobRepository;


    public GetJobByIdQueryHandler(ICacheService cacheService, IJobRepository jobRepository)
    {
        _cacheService = cacheService;
        _jobRepository = jobRepository;
    }

    public async Task<CommonResult<GetJobDto>> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
    {
        var cachedEntity = await _cacheService.GetData<GetJobDto>(CacheKeys.Job + request.Id, cancellationToken);

        if (cachedEntity is null)
        {
            var job = await _jobRepository.GetAsync(request.Id, cancellationToken);

            if (job is null)
                return Failure<GetJobDto>(CommonErrors.EntityDoesNotExist);

            var entityDto = job.MapJobToGetJobDto();

            cachedEntity = entityDto;

            await _cacheService.SetData(CacheKeys.DogOwner + job.Id, entityDto, cancellationToken);
        }

        return Success(cachedEntity);
    }
}