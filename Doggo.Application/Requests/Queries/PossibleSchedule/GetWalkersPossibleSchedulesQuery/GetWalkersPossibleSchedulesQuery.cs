namespace Doggo.Application.Requests.Queries.PossibleSchedule.GetWalkersPossibleSchedulesQuery;

using Domain.Results;
using MediatR;
using Responses;
using Responses.Walker.PossibleSchedule;

public record GetWalkersPossibleSchedulesQuery(Guid WalkerId) : IRequest<CommonResult<List<PossibleScheduleResponse>>>;