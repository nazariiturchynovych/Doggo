namespace Doggo.Application.Requests.Queries.Walker;

using Domain.Constants.ErrorConstants;
using Domain.DTO.Walker;
using Domain.Entities.Walker;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public record GetWalkerByIdQuery(int Id) : IRequest<CommonResult<GetWalkerDto>>
{
    public class Handler : IRequestHandler<GetWalkerByIdQuery, CommonResult<GetWalkerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public Handler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<CommonResult<GetWalkerDto>> Handle(GetWalkerByIdQuery request, CancellationToken cancellationToken)
        {

            var cachedEntity = await _cacheService.GetData<Walker>(request.Id.ToString());

            if (cachedEntity is null)
            {
                var repository = _unitOfWork.GetWalkerRepository();

                var dogOwner = await repository.GetAsync(request.Id, cancellationToken);

                if (dogOwner is null)
                    return Failure<GetWalkerDto>(CommonErrors.EntityDoesNotExist);

                await _cacheService.SetData(dogOwner.Id.ToString(), dogOwner);

                cachedEntity = dogOwner;
            }

            var entityDto = cachedEntity.MapWalkerToGetWalkerDto();

            return Success(entityDto);
        }
    }
}