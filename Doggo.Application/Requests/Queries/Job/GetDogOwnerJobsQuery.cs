namespace Doggo.Application.Requests.Queries.Job;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Job;
using Mappers;
using MediatR;

public record GetDogOwnerJobsQuery(Guid DogOwnerId) : IRequest<CommonResult<PageOfTDataDto<GetJobDto>>>
{
    public class Handler : IRequestHandler<GetDogOwnerJobsQuery, CommonResult<PageOfTDataDto<GetJobDto>>>
    {
        private readonly IJobRepository _jobRepository;


        public Handler(IJobRepository jobRepository)
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
}