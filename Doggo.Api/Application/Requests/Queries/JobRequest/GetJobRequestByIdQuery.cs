namespace Doggo.Application.Requests.Queries.JobRequest;

using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.DTO.JobRequest;
using Domain.Entities.JobRequest;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public record GetJobRequestByIdQuery(Guid Id) : IRequest<CommonResult<GetJobRequestDto>>
{
    public class Handler : IRequestHandler<GetJobRequestByIdQuery, CommonResult<GetJobRequestDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public Handler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<CommonResult<GetJobRequestDto>> Handle(
            GetJobRequestByIdQuery request,
            CancellationToken cancellationToken)
        {
            var cachedEntity = await _cacheService.GetData<JobRequest>(CacheKeys.JobRequest + request.Id);

            if (cachedEntity is null)
            {
                var repository = _unitOfWork.GetJobRequestRepository();

                var jobRequest = await repository.GetAsync(request.Id, cancellationToken);

                if (jobRequest is null)
                    return Failure<GetJobRequestDto>(CommonErrors.EntityDoesNotExist);

                await _cacheService.SetData(CacheKeys.JobRequest + request.Id, jobRequest);

                cachedEntity = jobRequest;
            }

            var entityDto = cachedEntity.MapJobRequestToGetJobRequestDto();

            return Success(entityDto);
        }
    }
};