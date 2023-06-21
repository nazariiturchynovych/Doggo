namespace Doggo.Application.Requests.Queries.Job;

using Domain.DTO;
using Domain.DTO.Job;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetPageOfJobsQuery(
    string? CommentSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Count,
    int PageCount) : IRequest<CommonResult<PageOfTDataDto<GetJobDto>>>
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

            var page = await userRepository.GetPageOfJobsAsync(
                request.CommentSearchTerm,
                request.SortColumn,
                request.SortOrder,
                request.Count,
                request.PageCount,
                cancellationToken);

            return Success(page.MapJobCollectionToPageOJobDto());
        }
    };
};