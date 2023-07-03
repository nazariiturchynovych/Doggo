namespace Doggo.Application.Requests.Queries.Walker;

using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO.Walker;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public record GetCurrentWalkerQuery(Guid UserId) : IRequest<CommonResult<GetWalkerDto>>
{
    public class Handler : IRequestHandler<GetCurrentWalkerQuery, CommonResult<GetWalkerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public Handler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<CommonResult<GetWalkerDto>> Handle(GetCurrentWalkerQuery request, CancellationToken cancellationToken)
        {

            var cachedEntity = await _cacheService.GetData<GetWalkerDto>(CacheKeys.Walker + request.UserId);

            if (cachedEntity is null)
            {
                var repository = _unitOfWork.GetWalkerRepository();

                var walker = await repository.GetByUserIdAsync(request.UserId, cancellationToken);

                if (walker is null)
                    return Failure<GetWalkerDto>(CommonErrors.EntityDoesNotExist);

                var entityDto = walker.MapWalkerToGetWalkerDto();

                cachedEntity = entityDto;

                await _cacheService.SetData(CacheKeys.DogOwner + walker.Id, entityDto);
            }

            return Success(cachedEntity);
        }
    }
}