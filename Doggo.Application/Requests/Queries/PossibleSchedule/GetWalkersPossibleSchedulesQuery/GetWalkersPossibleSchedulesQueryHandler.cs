namespace Doggo.Application.Requests.Queries.PossibleSchedule.GetWalkersPossibleSchedulesQuery;

using Abstractions.Repositories;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;
using Responses;
using Responses.Walker.PossibleSchedule;

public class GetWalkersPossibleSchedulesQueryHandler : IRequestHandler<GetWalkersPossibleSchedulesQuery, CommonResult<List<PossibleScheduleResponse>>>
{
    private readonly IPossibleScheduleRepository _possibleScheduleRepository;

    public GetWalkersPossibleSchedulesQueryHandler(IPossibleScheduleRepository possibleScheduleRepository)
    {
        _possibleScheduleRepository = possibleScheduleRepository;
    }

    public async Task<CommonResult<List<PossibleScheduleResponse>>> Handle(
        GetWalkersPossibleSchedulesQuery request,
        CancellationToken cancellationToken)
    {
        var possibleSchedules
            = await _possibleScheduleRepository.GetWalkerPossibleSchedulesAsync(request.WalkerId, cancellationToken);

        if (!possibleSchedules.Any())
            return Failure<List<PossibleScheduleResponse>>(CommonErrors.EntityDoesNotExist);

        return Success(possibleSchedules.MapPossibleScheduleCollectionToListOPossibleSchedulesResponse());
    }
}