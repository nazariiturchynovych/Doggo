namespace Doggo.Application.Requests.Queries.PossibleSchedule.GetWalkersPossibleSchedulesQuery;

using Domain.Results;
using DTO;
using DTO.Walker.PossibleSchedule;
using MediatR;

public record GetWalkersPossibleSchedulesQuery(Guid WalkerId) : IRequest<CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>>;