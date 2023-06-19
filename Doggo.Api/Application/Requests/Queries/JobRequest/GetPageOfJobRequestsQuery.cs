namespace Doggo.Application.Requests.Queries.JobRequest;

using Domain.DTO;
using Domain.DTO.JobRequest;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetPageOfJobRequestsQuery(int Count, int Page) : IRequest<CommonResult<PageOfTDataDto<GetJobRequestDto>>>
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
            var userRepository = _unitOfWork.GetJobRequestRepository();

            var page = await userRepository.GetPageOfJobRequestsAsync(request.Count, request.Page, cancellationToken);

            return Success(page.MapJobRequestCollectionToPageOJobRequestsDto());
        }
    };
};