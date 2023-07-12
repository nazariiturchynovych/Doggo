namespace Doggo.Application.Requests.Queries.PossibleSchedule.GetPageOfPossibleSchedulesQuery;

using Abstractions.Repositories;
using Domain.Results;
using Mappers;
using MediatR;
using Responses;
using Responses.Walker.PossibleSchedule;

public class GetPageOfPossibleSchedulesQueryHandler : IRequestHandler<GetPageOfPossibleSchedulesQuery, CommonResult<PageOf<PossibleScheduleResponse>>>
{
    private readonly IPossibleScheduleRepository _possibleScheduleRepository;

    public GetPageOfPossibleSchedulesQueryHandler(IPossibleScheduleRepository possibleScheduleRepository)
    {
        _possibleScheduleRepository = possibleScheduleRepository;
    }

    public async Task<CommonResult<PageOf<PossibleScheduleResponse>>> Handle(
        GetPageOfPossibleSchedulesQuery request,
        CancellationToken cancellationToken)
    {
        var page = await _possibleScheduleRepository.GetPageOfPossibleSchedulesAsync(
            request.PageCount,
            request.Page,
            cancellationToken);

        return Success(page.MapPossibleScheduleCollectionToPageOPossibleSchedulesResponse());
    }
}