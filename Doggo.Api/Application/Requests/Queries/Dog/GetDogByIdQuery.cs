namespace Doggo.Application.Requests.Queries.Dog;

using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.DTO.Dog;
using Domain.Entities.DogOwner;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public record GetDogByIdQuery(Guid Id) : IRequest<CommonResult<GetDogDto>>
{
    public class Handler : IRequestHandler<GetDogByIdQuery, CommonResult<GetDogDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public Handler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<CommonResult<GetDogDto>> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {

            var cachedEntity = await _cacheService.GetData<Dog>(CacheKeys.Dog + request.Id);

            if (cachedEntity is null)
            {
                var repository = _unitOfWork.GetDogRepository();

                var dog = await repository.GetAsync(request.Id, cancellationToken);

                if (dog is null)
                    return Failure<GetDogDto>(CommonErrors.EntityDoesNotExist);

                await _cacheService.SetData(CacheKeys.Dog + dog.Id, dog);

                cachedEntity = dog;
            }

            var entityDto = cachedEntity.MapDogToGetDogDto();

            return Success(entityDto);
        }
    }
};