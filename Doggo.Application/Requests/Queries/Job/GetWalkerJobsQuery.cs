namespace Doggo.Application.Requests.Queries.Job;

using Domain.Results;
using DTO;
using DTO.Job;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetWalkerJobsQuery(Guid WalkerId) : IRequest<CommonResult<PageOfTDataDto<GetJobDto>>>
{
    public class Handler : IRequestHandler<GetWalkerJobsQuery, CommonResult<PageOfTDataDto<GetJobDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetJobDto>>> Handle(
            GetWalkerJobsQuery request,
            CancellationToken cancellationToken)
        {
            var jobRepository = _unitOfWork.GetJobRepository();

            var page = await jobRepository.GetWalkerJobsAsync(request.WalkerId, cancellationToken);

            return Success(page.MapJobCollectionToPageOJobDto());
        }
    }
}