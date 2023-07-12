namespace Doggo.Application.Requests.Queries.PossibleSchedule.GetPossibleScheduleByIdQuery;

using Domain.Results;
using MediatR;
using Responses.Walker.PossibleSchedule;

public record GetPossibleScheduleByIdQuery(Guid Id) : IRequest<CommonResult<PossibleScheduleResponse>>;