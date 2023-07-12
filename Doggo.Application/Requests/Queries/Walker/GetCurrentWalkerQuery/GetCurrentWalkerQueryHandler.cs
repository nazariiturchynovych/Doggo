namespace Doggo.Application.Requests.Queries.Walker.GetCurrentWalkerQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.Walker;

public class GetCurrentWalkerQueryHandler : IRequestHandler<GetCurrentWalkerQuery, CommonResult<WalkerResponse>>
{
    private readonly ICacheService _cacheService;
    private readonly IWalkerRepository _walkerRepository;

    public GetCurrentWalkerQueryHandler(ICacheService cacheService, IWalkerRepository walkerRepository)
    {
        _cacheService = cacheService;
        _walkerRepository = walkerRepository;
    }

    public async Task<CommonResult<WalkerResponse>> Handle(GetCurrentWalkerQuery request, CancellationToken cancellationToken)
    {
        var cachedEntity = await _cacheService.GetData<WalkerResponse>(CacheKeys.Walker + request.UserId, cancellationToken);

        if (cachedEntity is null)
        {
            var walker = await _walkerRepository.GetByUserIdAsync(request.UserId, cancellationToken);

            if (walker is null)
                return Failure<WalkerResponse>(CommonErrors.EntityDoesNotExist);

            var entityDto = walker.MapWalkerToWalkerResponse();

            cachedEntity = entityDto;

            await _cacheService.SetData(CacheKeys.DogOwner + walker.Id, entityDto, cancellationToken);
        }

        return Success(cachedEntity);
    }
}