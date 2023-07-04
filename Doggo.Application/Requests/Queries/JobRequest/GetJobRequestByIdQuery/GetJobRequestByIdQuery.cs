namespace Doggo.Application.Requests.Queries.JobRequest.GetJobRequestByIdQuery;

using Domain.Results;
using DTO.JobRequest;
using MediatR;

public record GetJobRequestByIdQuery(Guid Id) : IRequest<CommonResult<GetJobRequestDto>>;