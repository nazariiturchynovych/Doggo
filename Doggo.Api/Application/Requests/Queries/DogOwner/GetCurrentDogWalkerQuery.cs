namespace Doggo.Application.Requests.Queries.DogOwner;

using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.DTO.DogOwner;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public record GetCurrentDogWalkerQuery(Guid UserId) : IRequest<CommonResult<GetDogOwnerDto>>
{
    public class Handler : IRequestHandler<GetCurrentDogWalkerQuery, CommonResult<GetDogOwnerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public Handler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<CommonResult<GetDogOwnerDto>> Handle(GetCurrentDogWalkerQuery request, CancellationToken cancellationToken)
        {

            var cachedEntity = await _cacheService.GetData<GetDogOwnerDto>(CacheKeys.DogOwner + request.UserId);

            if (cachedEntity is null)
            {
                var repository = _unitOfWork.GetDogOwnerRepository();

                var dogOwner = await repository.GetByUserIdAsync(request.UserId, cancellationToken);

                if (dogOwner is null)
                    return Failure<GetDogOwnerDto>(CommonErrors.EntityDoesNotExist);

                var entityDto = dogOwner.MapDogOwnerToGetDogOwnerDto();

                cachedEntity = entityDto;

                await _cacheService.SetData(CacheKeys.DogOwner + dogOwner.Id, entityDto);
            }

            return Success(cachedEntity);
        }
    }
};