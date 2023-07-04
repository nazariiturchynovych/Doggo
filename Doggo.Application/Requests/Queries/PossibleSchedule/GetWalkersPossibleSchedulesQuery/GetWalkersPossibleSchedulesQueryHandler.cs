namespace Doggo.Application.Requests.Queries.PossibleSchedule.GetWalkersPossibleSchedulesQuery;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO;
using DTO.Walker.PossibleSchedule;
using Mappers;
using MediatR;

public class GetWalkersPossibleSchedulesQueryHandler : IRequestHandler<GetWalkersPossibleSchedulesQuery, CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>>
{
    private readonly IPossibleScheduleRepository _possibleScheduleRepository;

    public GetWalkersPossibleSchedulesQueryHandler(IPossibleScheduleRepository possibleScheduleRepository)
    {
        _possibleScheduleRepository = possibleScheduleRepository;
    }

    public async Task<CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>> Handle(
        GetWalkersPossibleSchedulesQuery request,
        CancellationToken cancellationToken)
    {
        var possibleSchedules
            = await _possibleScheduleRepository.GetWalkerPossibleSchedulesAsync(request.WalkerId, cancellationToken);

        if (!possibleSchedules.Any())
            return Failure<PageOfTDataDto<GetPossibleScheduleDto>>(CommonErrors.EntityDoesNotExist);

        return Success(possibleSchedules.MapPossibleScheduleCollectionToPageOPossibleSchedulesDto());
    }
}