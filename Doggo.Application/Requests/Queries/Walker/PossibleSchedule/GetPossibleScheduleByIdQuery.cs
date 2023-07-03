namespace Doggo.Application.Requests.Queries.Walker.PossibleSchedule;

using Abstractions.Persistence.Read;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.Walker.Schedule;
using Domain.Results;
using DTO.Walker.PossibleSchedule;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public record GetPossibleScheduleByIdQuery(Guid Id) : IRequest<CommonResult<GetPossibleScheduleDto>>
{
    public class Handler : IRequestHandler<GetPossibleScheduleByIdQuery, CommonResult<GetPossibleScheduleDto>>
    {
        private readonly ICacheService _cacheService;
        private readonly IPossibleScheduleRepository _possibleScheduleRepository;

        public Handler(ICacheService cacheService, IPossibleScheduleRepository possibleScheduleRepository)
        {
            _cacheService = cacheService;
            _possibleScheduleRepository = possibleScheduleRepository;
        }

        public async Task<CommonResult<GetPossibleScheduleDto>> Handle(
            GetPossibleScheduleByIdQuery request,
            CancellationToken cancellationToken)
        {
            var cachedEntity = await _cacheService.GetData<PossibleSchedule>(
                CacheKeys.PossibleSchedule + request.Id,
                cancellationToken);

            if (cachedEntity is null)
            {
                var possibleSchedule = await _possibleScheduleRepository.GetAsync(request.Id, cancellationToken);

                if (possibleSchedule is null)
                    return Failure<GetPossibleScheduleDto>(CommonErrors.EntityDoesNotExist);

                await _cacheService.SetData(CacheKeys.PossibleSchedule + request.Id, possibleSchedule, cancellationToken);

                cachedEntity = possibleSchedule;
            }

            var entityDto = cachedEntity.MapPossibleScheduleToGetPossibleScheduleDto();

            return Success(entityDto);
        }
    }
}