namespace Doggo.Application.Requests.Queries.JobRequest;

using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.DTO.JobRequest;
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
            var cachedEntity = await _cacheService.GetData<GetJobRequestDto>(CacheKeys.JobRequest + request.Id);

            if (cachedEntity is null)
            {
                var repository = _unitOfWork.GetJobRequestRepository();

                var jobRequest = await repository.GetAsync(request.Id, cancellationToken);

                if (jobRequest is null)
                    return Failure<GetJobRequestDto>(CommonErrors.EntityDoesNotExist);

                var entityDto = jobRequest.MapJobRequestToGetJobRequestDto();

                cachedEntity = entityDto;

                await _cacheService.SetData(CacheKeys.DogOwner + jobRequest.Id, entityDto);
            }

            return Success(cachedEntity);
        }
    }
};