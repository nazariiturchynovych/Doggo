namespace Doggo.Application.Requests.Queries.Walker;

using Abstractions.Persistence.Read;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO.Walker;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public record GetCurrentWalkerQuery(Guid UserId) : IRequest<CommonResult<GetWalkerDto>>
{
    public class Handler : IRequestHandler<GetCurrentWalkerQuery, CommonResult<GetWalkerDto>>
    {
        private readonly ICacheService _cacheService;
        private readonly IWalkerRepository _walkerRepository;

        public Handler(ICacheService cacheService, IWalkerRepository walkerRepository)
        {
            _cacheService = cacheService;
            _walkerRepository = walkerRepository;
        }

        public async Task<CommonResult<GetWalkerDto>> Handle(GetCurrentWalkerQuery request, CancellationToken cancellationToken)
        {
            var cachedEntity = await _cacheService.GetData<GetWalkerDto>(CacheKeys.Walker + request.UserId, cancellationToken);

            if (cachedEntity is null)
            {
                var walker = await _walkerRepository.GetByUserIdAsync(request.UserId, cancellationToken);

                if (walker is null)
                    return Failure<GetWalkerDto>(CommonErrors.EntityDoesNotExist);

                var entityDto = walker.MapWalkerToGetWalkerDto();

                cachedEntity = entityDto;

                await _cacheService.SetData(CacheKeys.DogOwner + walker.Id, entityDto, cancellationToken);
            }

            return Success(cachedEntity);
        }
    }
}