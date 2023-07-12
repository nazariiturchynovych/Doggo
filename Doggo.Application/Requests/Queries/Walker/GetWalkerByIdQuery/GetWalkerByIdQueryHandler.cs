namespace Doggo.Application.Requests.Queries.Walker.GetWalkerByIdQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.Walker;

public class GetWalkerByIdQueryHandler : IRequestHandler<GetWalkerByIdQuery, CommonResult<WalkerResponse>>
{
    private readonly ICacheService _cacheService;
    private readonly IWalkerRepository _walkerRepository;


    public GetWalkerByIdQueryHandler(ICacheService cacheService, IWalkerRepository walkerRepository)
    {
        _cacheService = cacheService;
        _walkerRepository = walkerRepository;
    }

    public async Task<CommonResult<WalkerResponse>> Handle(GetWalkerByIdQuery request, CancellationToken cancellationToken)
    {
        var cachedEntity = await _cacheService.GetData<WalkerResponse>(CacheKeys.Walker + request.Id, cancellationToken);

        if (cachedEntity is not null)
            return Success(cachedEntity);

        var walker = await _walkerRepository.GetAsync(request.Id, cancellationToken);

        if (walker is null)
            return Failure<WalkerResponse>(CommonErrors.EntityDoesNotExist);

        var entityDto = walker.MapWalkerToWalkerResponse();

        cachedEntity = entityDto;

        await _cacheService.SetData(CacheKeys.DogOwner + walker.Id, entityDto, cancellationToken);

        return Success(cachedEntity);
    }
}