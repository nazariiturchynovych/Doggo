namespace Doggo.Application.Requests.Queries.DogOwner;

using Domain.Constants.ErrorConstants;
using Domain.DTO.DogOwner;
using Domain.Entities.DogOwner;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public record GetDogOwnerByIdQuery(int Id) : IRequest<CommonResult<GetDogOwnerDto>>
{
    public class Handler : IRequestHandler<GetDogOwnerByIdQuery, CommonResult<GetDogOwnerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public Handler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<CommonResult<GetDogOwnerDto>> Handle(GetDogOwnerByIdQuery request, CancellationToken cancellationToken)
        {

            var cachedEntity = await _cacheService.GetData<DogOwner>(request.Id.ToString());

            if (cachedEntity is null)
            {
                var repository = _unitOfWork.GetDogOwnerRepository();

                var dogOwner = await repository.GetAsync(request.Id, cancellationToken);

                if (dogOwner is null)
                    return Failure<GetDogOwnerDto>(CommonErrors.EntityDoesNotExist);

                await _cacheService.SetData(dogOwner.Id.ToString(), dogOwner);

                cachedEntity = dogOwner;
            }

            var getUserDto = cachedEntity.MapDogOwnerToGetDogOwnerDto();

            return Success(getUserDto);
        }
    }
};