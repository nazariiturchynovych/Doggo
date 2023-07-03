namespace Doggo.Application.Requests.Queries.Job;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Job;
using Mappers;
using MediatR;

public record GetDogJobsQuery(Guid DogId) : IRequest<CommonResult<PageOfTDataDto<GetJobDto>>>
{
    public class Handler : IRequestHandler<GetDogJobsQuery, CommonResult<PageOfTDataDto<GetJobDto>>>
    {
        private readonly IJobRepository _jobRepository;

        public Handler(IJobRepository jobRepository)
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
}