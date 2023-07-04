namespace Doggo.Application.Requests.Queries.PossibleSchedule.GetPossibleScheduleByIdQuery;

using Domain.Results;
using DTO.Walker.PossibleSchedule;
using MediatR;

public record GetPossibleScheduleByIdQuery(Guid Id) : IRequest<CommonResult<GetPossibleScheduleDto>>;