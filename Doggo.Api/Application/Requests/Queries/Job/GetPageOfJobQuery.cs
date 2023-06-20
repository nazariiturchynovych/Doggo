namespace Doggo.Application.Requests.Queries.Job;

using Domain.DTO;
using Domain.DTO.Job;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetPageOfJobsQuery(int Count, int Page) : IRequest<CommonResult<PageOfTDataDto<GetJobDto>>>
{
    public class Handler : IRequestHandler<GetPageOfJobsQuery, CommonResult<PageOfTDataDto<GetJobDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetJobDto>>> Handle(
            GetPageOfJobsQuery request,
            CancellationToken cancellationToken)
        {
            var userRepository = _unitOfWork.GetJobRepository();

            var page = await userRepository.GetPageOfJobsAsync(request.Count, request.Page, cancellationToken);

            return Success(page.MapJobCollectionToPageOJobDto());
        }
    };
};