namespace Doggo.Application.Requests.Queries.Dog.GetDogByIdQuery;

using Abstractions.Persistence.Read;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO.Dog;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, CommonResult<GetDogDto>>
{
    private readonly ICacheService _cacheService;
    private readonly IDogRepository _dogRepository;

    public GetDogByIdQueryHandler(ICacheService cacheService, IDogRepository dogRepository)
    {
        _cacheService = cacheService;
        _dogRepository = dogRepository;
    }

    public async Task<CommonResult<GetDogDto>> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
    {
        var cachedEntity = await _cacheService.GetData<GetDogDto>(CacheKeys.Dog + request.Id, cancellationToken);

        if (cachedEntity is null)
        {

            var dog = await _dogRepository.GetAsync(request.Id, cancellationToken);

            if (dog is null)
                return Failure<GetDogDto>(CommonErrors.EntityDoesNotExist);

            var entityDto = dog.MapDogToGetDogDto();

            cachedEntity = entityDto;

            await _cacheService.SetData(CacheKeys.DogOwner + dog.Id, entityDto, cancellationToken);
        }

        return Success(cachedEntity);
    }
}