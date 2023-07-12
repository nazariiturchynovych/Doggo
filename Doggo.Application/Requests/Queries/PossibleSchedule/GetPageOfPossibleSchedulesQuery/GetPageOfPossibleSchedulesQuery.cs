namespace Doggo.Application.Requests.Queries.PossibleSchedule.GetPageOfPossibleSchedulesQuery;

using Domain.Results;
using MediatR;
using Responses;
using Responses.Walker.PossibleSchedule;

public record GetPageOfPossibleSchedulesQuery(int PageCount, int Page)
    : IRequest<CommonResult<PageOf<PossibleScheduleResponse>>>;