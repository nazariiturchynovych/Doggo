namespace Doggo.Application.Requests.Queries.Job.GetJobByIdQuery;

using Domain.Results;
using DTO.Job;
using MediatR;

public record GetJobByIdQuery(Guid Id) : IRequest<CommonResult<GetJobDto>>;