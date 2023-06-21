namespace Doggo.Application.Requests.Queries.Job;

using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.DTO.Job;
using Domain.Entities.Job;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public record GetJobByIdQuery(Guid Id) : IRequest<CommonResult<GetJobDto>>
{
    public class Handler : IRequestHandler<GetJobByIdQuery, CommonResult<GetJobDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public Handler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<CommonResult<GetJobDto>> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var cachedEntity = await _cacheService.GetData<Job>(CacheKeys.Job + request.Id);

            if (cachedEntity is null)
            {
                var repository = _unitOfWork.GetJobRepository();

                var dog = await repository.GetAsync(request.Id, cancellationToken);

                if (dog is null)
                    return Failure<GetJobDto>(CommonErrors.EntityDoesNotExist);

                await _cacheService.SetData(CacheKeys.Job + request.Id, dog);

                cachedEntity = dog;
            }

            var entityDto = cachedEntity.MapJobToGetJobDto();

            return Success(entityDto);
        }
    }
}