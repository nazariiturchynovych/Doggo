namespace Doggo.Application.Requests.Queries.PossibleSchedule.GetPageOfPossibleSchedulesQuery;

using Domain.Results;
using DTO;
using DTO.Walker.PossibleSchedule;
using MediatR;

public record GetPageOfPossibleSchedulesQuery(int PageCount, int Page)
    : IRequest<CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>>;