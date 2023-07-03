namespace Doggo.Application.Requests.Queries.Walker;

using Abstractions.Persistence.Read;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO.Walker;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public record GetWalkerByIdQuery(Guid Id) : IRequest<CommonResult<GetWalkerDto>>
{
    public class Handler : IRequestHandler<GetWalkerByIdQuery, CommonResult<GetWalkerDto>>
    {
        private readonly ICacheService _cacheService;
        private readonly IWalkerRepository _walkerRepository;


        public Handler(ICacheService cacheService, IWalkerRepository walkerRepository)
        {
            _cacheService = cacheService;
            _walkerRepository = walkerRepository;
        }

        public async Task<CommonResult<GetWalkerDto>> Handle(GetWalkerByIdQuery request, CancellationToken cancellationToken)
        {
            var cachedEntity = await _cacheService.GetData<GetWalkerDto>(CacheKeys.Walker + request.Id, cancellationToken);

            if (cachedEntity is not null)
                return Success(cachedEntity);

            var walker = await _walkerRepository.GetAsync(request.Id, cancellationToken);

            if (walker is null)
                return Failure<GetWalkerDto>(CommonErrors.EntityDoesNotExist);

            var entityDto = walker.MapWalkerToGetWalkerDto();

            cachedEntity = entityDto;

            await _cacheService.SetData(CacheKeys.DogOwner + walker.Id, entityDto, cancellationToken);

            return Success(cachedEntity);
        }
    }
}