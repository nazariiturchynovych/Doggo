namespace Doggo.Application.Requests.Queries.Job;

using Domain.Results;
using DTO;
using DTO.Job;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetDogJobsQuery(Guid DogId) : IRequest<CommonResult<PageOfTDataDto<GetJobDto>>>
{
    public class Handler : IRequestHandler<GetDogJobsQuery, CommonResult<PageOfTDataDto<GetJobDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetJobDto>>> Handle(
            GetDogJobsQuery request,
            CancellationToken cancellationToken)
        {
            var jobRepository = _unitOfWork.GetJobRepository();

            var page = await jobRepository.GetDogJobsAsync(request.DogId, cancellationToken);

            return Success(page.MapJobCollectionToPageOJobDto());
        }
    }
}