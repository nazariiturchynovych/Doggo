namespace Doggo.Application.Requests.Queries.DogOwner.GetCurrentDogOwnerQuery;

using Abstractions.Persistence.Read;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO.DogOwner;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public class GetCurrentDogOwnerQueryHandler : IRequestHandler<GetCurrentDogOwnerQuery, CommonResult<GetDogOwnerDto>>
{
    private readonly ICacheService _cacheService;
    private readonly IDogOwnerRepository _dogOwnerRepository;

    public GetCurrentDogOwnerQueryHandler(ICacheService cacheService, IDogOwnerRepository dogOwnerRepository)
    {
        _cacheService = cacheService;
        _dogOwnerRepository = dogOwnerRepository;
    }

    public async Task<CommonResult<GetDogOwnerDto>> Handle(GetCurrentDogOwnerQuery request, CancellationToken cancellationToken)
    {

        var cachedEntity = await _cacheService.GetData<GetDogOwnerDto>(CacheKeys.DogOwner + request.UserId, cancellationToken);

        if (cachedEntity is null)
        {

            var dogOwner = await _dogOwnerRepository.GetByUserIdAsync(request.UserId, cancellationToken);

            if (dogOwner is null)
                return Failure<GetDogOwnerDto>(CommonErrors.EntityDoesNotExist);

            var entityDto = dogOwner.MapDogOwnerToGetDogOwnerDto();

            cachedEntity = entityDto;

            await _cacheService.SetData(CacheKeys.DogOwner + dogOwner.Id, entityDto, cancellationToken);
        }

        return Success(cachedEntity);
    }
}