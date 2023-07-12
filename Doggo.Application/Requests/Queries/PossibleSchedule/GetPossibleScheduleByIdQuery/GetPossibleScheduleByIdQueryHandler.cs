namespace Doggo.Application.Requests.Queries.PossibleSchedule.GetPossibleScheduleByIdQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.Walker.Schedule;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.Walker.PossibleSchedule;

public class GetPossibleScheduleByIdQueryHandler : IRequestHandler<GetPossibleScheduleByIdQuery, CommonResult<PossibleScheduleResponse>>
{
    private readonly ICacheService _cacheService;
    private readonly IPossibleScheduleRepository _possibleScheduleRepository;

    public GetPossibleScheduleByIdQueryHandler(ICacheService cacheService, IPossibleScheduleRepository possibleScheduleRepository)
    {
        _cacheService = cacheService;
        _possibleScheduleRepository = possibleScheduleRepository;
    }

    public async Task<CommonResult<PossibleScheduleResponse>> Handle(
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
                return Failure<PossibleScheduleResponse>(CommonErrors.EntityDoesNotExist);

            await _cacheService.SetData(CacheKeys.PossibleSchedule + request.Id, possibleSchedule, cancellationToken);

            cachedEntity = possibleSchedule;
        }

        var entityDto = cachedEntity.MapPossibleScheduleToPossibleScheduleResponse();

        return Success(entityDto);
    }
}