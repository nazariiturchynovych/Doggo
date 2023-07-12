namespace Doggo.Application.Requests.Queries.Job.GetDogJobsQuery;

using Abstractions.Repositories;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.Job;

public class GetDogJobsQueryHandler : IRequestHandler<GetDogJobsQuery, CommonResult<List<JobResponse>>>
{
    private readonly IJobRepository _jobRepository;

    public GetDogJobsQueryHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<CommonResult<List<JobResponse>>> Handle(
        GetDogJobsQuery request,
        CancellationToken cancellationToken)
    {
        var page = await _jobRepository.GetDogJobsAsync(request.DogId, cancellationToken);

        return Success(page.MapJobCollectionToListJobResponse());
    }
}