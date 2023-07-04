namespace Doggo.Application.Requests.Queries.PossibleSchedule.GetPageOfPossibleSchedulesQuery;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Walker.PossibleSchedule;
using Mappers;
using MediatR;

public class GetPageOfPossibleSchedulesQueryHandler : IRequestHandler<GetPageOfPossibleSchedulesQuery, CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>>
{
    private readonly IPossibleScheduleRepository _possibleScheduleRepository;

    public GetPageOfPossibleSchedulesQueryHandler(IPossibleScheduleRepository possibleScheduleRepository)
    {
        _possibleScheduleRepository = possibleScheduleRepository;
    }

    public async Task<CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>> Handle(
        GetPageOfPossibleSchedulesQuery request,
        CancellationToken cancellationToken)
    {
        var page = await _possibleScheduleRepository.GetPageOfPossibleSchedulesAsync(
            request.PageCount,
            request.Page,
            cancellationToken);

        return Success(page.MapPossibleScheduleCollectionToPageOPossibleSchedulesDto());
    }
}