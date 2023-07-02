namespace Doggo.Api.Application.Requests.Queries.Walker;

using Doggo.Application.Mappers;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.DTO.Walker;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CacheService;
using MediatR;

public record GetWalkerByIdQuery(Guid Id) : IRequest<CommonResult<GetWalkerDto>>
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
            var cachedEntity = await _cacheService.GetData<GetWalkerDto>(CacheKeys.Walker + request.Id);

            if (cachedEntity is not null)
                return Success(cachedEntity);

            var repository = _unitOfWork.GetWalkerRepository();

            var walker = await repository.GetAsync(request.Id, cancellationToken);

            if (walker is null)
                return Failure<GetWalkerDto>(CommonErrors.EntityDoesNotExist);

            var entityDto = walker.MapWalkerToGetWalkerDto();

            cachedEntity = entityDto;

            await _cacheService.SetData(CacheKeys.DogOwner + walker.Id, entityDto);

            return Success(cachedEntity);
        }
    }
}