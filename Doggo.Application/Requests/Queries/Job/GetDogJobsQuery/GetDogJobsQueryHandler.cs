namespace Doggo.Application.Requests.Queries.Job.GetDogJobsQuery;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Job;
using Mappers;
using MediatR;

public class GetDogJobsQueryHandler : IRequestHandler<GetDogJobsQuery, CommonResult<PageOfTDataDto<GetJobDto>>>
{
    private readonly IJobRepository _jobRepository;

    public GetDogJobsQueryHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<CommonResult<PageOfTDataDto<GetJobDto>>> Handle(
        GetDogJobsQuery request,
        CancellationToken cancellationToken)
    {
        var page = await _jobRepository.GetDogJobsAsync(request.DogId, cancellationToken);

        return Success(page.MapJobCollectionToPageOJobDto());
    }
}