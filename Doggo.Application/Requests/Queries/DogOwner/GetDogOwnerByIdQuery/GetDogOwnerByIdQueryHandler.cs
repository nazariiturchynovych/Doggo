namespace Doggo.Application.Requests.Queries.DogOwner.GetDogOwnerByIdQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.DogOwner;

public class GetDogOwnerByIdQueryHandler : IRequestHandler<GetDogOwnerByIdQuery, CommonResult<DogOwnerResponse>>
{
    private readonly ICacheService _cacheService;
    private readonly IDogOwnerRepository _dogOwnerRepository;


    public GetDogOwnerByIdQueryHandler(ICacheService cacheService, IDogOwnerRepository dogOwnerRepository)
    {
        _cacheService = cacheService;
        _dogOwnerRepository = dogOwnerRepository;
    }

    public async Task<CommonResult<DogOwnerResponse>> Handle(GetDogOwnerByIdQuery request, CancellationToken cancellationToken)
    {

        var cachedEntity = await _cacheService.GetData<DogOwnerResponse>(CacheKeys.DogOwner + request.Id, cancellationToken);

        if (cachedEntity is null)
        {

            var dogOwner = await _dogOwnerRepository.GetAsync(request.Id, cancellationToken);

            if (dogOwner is null)
                return Failure<DogOwnerResponse>(CommonErrors.EntityDoesNotExist);

            var entityDto = dogOwner.MapDogOwnerToDogOwnerResponse();

            cachedEntity = entityDto;

            await _cacheService.SetData(CacheKeys.DogOwner + dogOwner.Id, entityDto, cancellationToken);
        }

        return Success(cachedEntity);
    }
}