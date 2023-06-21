namespace Doggo.Application.Requests.Queries.JobRequest;

using Domain.DTO;
using Domain.DTO.JobRequest;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetDogOwnerJobRequestsQuery(Guid DogOwnerId) : IRequest<CommonResult<PageOfTDataDto<GetJobRequestDto>>>
{
    public class Handler : IRequestHandler<GetDogOwnerJobRequestsQuery, CommonResult<PageOfTDataDto<GetJobRequestDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetJobRequestDto>>> Handle(
            GetDogOwnerJobRequestsQuery request,
            CancellationToken cancellationToken)
        {
            var jobRequestRepository = _unitOfWork.GetJobRequestRepository();

            var page = await jobRequestRepository.GetDogOwnerJobRequests(request.DogOwnerId, cancellationToken);

            return Success(page.MapJobRequestCollectionToPageOJobRequestsDto());
        }
    };
};