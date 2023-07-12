namespace Doggo.Application.Requests.Queries.Job.GetDogOwnerJobsQuery;

using Abstractions.Repositories;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.Job;

public class GetDogOwnerJobsQueryHandler : IRequestHandler<GetDogOwnerJobsQuery, CommonResult<List<JobResponse>>>
{
    private readonly IJobRepository _jobRepository;


    public GetDogOwnerJobsQueryHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<CommonResult<List<JobResponse>>> Handle(
        GetDogOwnerJobsQuery request,
        CancellationToken cancellationToken)
    {
        var page = await _jobRepository.GetDogOwnerJobsAsync(request.DogOwnerId, cancellationToken);

        return Success(page.MapJobCollectionToListJobResponse());
    }
}