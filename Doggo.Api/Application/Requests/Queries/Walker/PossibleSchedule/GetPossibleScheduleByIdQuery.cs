namespace Doggo.Application.Requests.Queries.Walker.PossibleSchedule;

using Domain.Constants.ErrorConstants;
using Domain.DTO.Walker.PossibleSchedule;
using Domain.Entities.Walker.Schedule;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public record GetPossibleScheduleByIdQuery(int Id) : IRequest<CommonResult<GetPossibleScheduleDto>>
{
    public class Handler : IRequestHandler<GetPossibleScheduleByIdQuery, CommonResult<GetPossibleScheduleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public Handler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<CommonResult<GetPossibleScheduleDto>> Handle(GetPossibleScheduleByIdQuery request, CancellationToken cancellationToken)
        {

            var cachedEntity = await _cacheService.GetData<PossibleSchedule>(request.Id.ToString());

            if (cachedEntity is null)
            {
                var repository = _unitOfWork.GetPossibleScheduleRepository();

                var possibleSchedule = await repository.GetAsync(request.Id, cancellationToken);

                if (possibleSchedule is null)
                    return Failure<GetPossibleScheduleDto>(CommonErrors.EntityDoesNotExist);

                await _cacheService.SetData(possibleSchedule.Id.ToString(), possibleSchedule);

                cachedEntity = possibleSchedule;
            }

            var entityDto = cachedEntity.MapPossibleScheduleToGetPossibleScheduleDto();

            return Success(entityDto);
        }
    }
}