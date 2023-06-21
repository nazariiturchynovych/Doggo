namespace Doggo.Application.Requests.Queries.Job;

using Domain.DTO;
using Domain.DTO.Job;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetDogOwnerJobsQuery(Guid DogOwnerId) : IRequest<CommonResult<PageOfTDataDto<GetJobDto>>>
{
    public class Handler : IRequestHandler<GetDogOwnerJobsQuery, CommonResult<PageOfTDataDto<GetJobDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetJobDto>>> Handle(
            GetDogOwnerJobsQuery request,
            CancellationToken cancellationToken)
        {
            var jobRepository = _unitOfWork.GetJobRepository();

            var page = await jobRepository.GetDogOwnerJobsAsync(request.DogOwnerId, cancellationToken);

            return Success(page.MapJobCollectionToPageOJobDto());
        }
    }
}