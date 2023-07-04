namespace Doggo.Application.Requests.Queries.Job.GetDogOwnerJobsQuery;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Job;
using Mappers;
using MediatR;

public class GetDogOwnerJobsQueryHandler : IRequestHandler<GetDogOwnerJobsQuery, CommonResult<PageOfTDataDto<GetJobDto>>>
{
    private readonly IJobRepository _jobRepository;


    public GetDogOwnerJobsQueryHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<CommonResult<PageOfTDataDto<GetJobDto>>> Handle(
        GetDogOwnerJobsQuery request,
        CancellationToken cancellationToken)
    {
        var page = await _jobRepository.GetDogOwnerJobsAsync(request.DogOwnerId, cancellationToken);

        return Success(page.MapJobCollectionToPageOJobDto());
    }
}