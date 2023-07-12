namespace Doggo.Application.Requests.Queries.DogOwner.GetCurrentDogOwnerQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.DogOwner;

public class GetCurrentDogOwnerQueryHandler : IRequestHandler<GetCurrentDogOwnerQuery, CommonResult<DogOwnerResponse>>
{
    private readonly ICacheService _cacheService;
    private readonly IDogOwnerRepository _dogOwnerRepository;

    public GetCurrentDogOwnerQueryHandler(ICacheService cacheService, IDogOwnerRepository dogOwnerRepository)
    {
        _cacheService = cacheService;
        _dogOwnerRepository = dogOwnerRepository;
    }

    public async Task<CommonResult<DogOwnerResponse>> Handle(GetCurrentDogOwnerQuery request, CancellationToken cancellationToken)
    {

        var cachedEntity = await _cacheService.GetData<DogOwnerResponse>(CacheKeys.DogOwner + request.UserId, cancellationToken);

        if (cachedEntity is null)
        {

            var dogOwner = await _dogOwnerRepository.GetByUserIdAsync(request.UserId, cancellationToken);

            if (dogOwner is null)
                return Failure<DogOwnerResponse>(CommonErrors.EntityDoesNotExist);

            var entityDto = dogOwner.MapDogOwnerToDogOwnerResponse();

            cachedEntity = entityDto;

            await _cacheService.SetData(CacheKeys.DogOwner + dogOwner.Id, entityDto, cancellationToken);
        }

        return Success(cachedEntity);
    }
}