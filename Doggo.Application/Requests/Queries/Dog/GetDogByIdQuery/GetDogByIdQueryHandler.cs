namespace Doggo.Application.Requests.Queries.Dog.GetDogByIdQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.Dog;

public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, CommonResult<DogResponse>>
{
    private readonly ICacheService _cacheService;
    private readonly IDogRepository _dogRepository;

    public GetDogByIdQueryHandler(ICacheService cacheService, IDogRepository dogRepository)
    {
        _cacheService = cacheService;
        _dogRepository = dogRepository;
    }

    public async Task<CommonResult<DogResponse>> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
    {
        var cachedEntity = await _cacheService.GetData<DogResponse>(CacheKeys.Dog + request.Id, cancellationToken);

        if (cachedEntity is null)
        {

            var dog = await _dogRepository.GetAsync(request.Id, cancellationToken);

            if (dog is null)
                return Failure<DogResponse>(CommonErrors.EntityDoesNotExist);

            var entityDto = dog.MapDogToDogResponse();

            cachedEntity = entityDto;

            await _cacheService.SetData(CacheKeys.DogOwner + dog.Id, entityDto, cancellationToken);
        }

        return Success(cachedEntity);
    }
}