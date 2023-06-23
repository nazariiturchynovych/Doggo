namespace Doggo.Application.Requests.Queries.JobRequest;

using Domain.DTO;
using Domain.DTO.JobRequest;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetPageOfJobRequestsQuery(
    string? DescriptionSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOfTDataDto<GetJobRequestDto>>>
{
    public class Handler : IRequestHandler<GetPageOfJobRequestsQuery, CommonResult<PageOfTDataDto<GetJobRequestDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetJobRequestDto>>> Handle(
            GetPageOfJobRequestsQuery request,
            CancellationToken cancellationToken)
        {
            var jobRequestRepository = _unitOfWork.GetJobRequestRepository();

            var page = await jobRequestRepository.GetPageOfJobRequestsAsync(request.DescriptionSearchTerm,
                request.SortColumn,
                request.SortOrder,
                request.PageCount,
                request.Page,
                cancellationToken);

            return Success(page.MapJobRequestCollectionToPageOJobRequestsDto());
        }
    };
};