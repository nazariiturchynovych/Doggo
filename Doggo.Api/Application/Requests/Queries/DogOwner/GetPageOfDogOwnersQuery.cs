namespace Doggo.Application.Requests.Queries.DogOwner;

using Domain.DTO;
using Domain.DTO.DogOwner;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetPageOfDogOwnersQuery(int Count, int Page) : IRequest<CommonResult<PageOfTDataDto<GetDogOwnerDto>>>
{
    public class Handler : IRequestHandler<GetPageOfDogOwnersQuery, CommonResult<PageOfTDataDto<GetDogOwnerDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetDogOwnerDto>>> Handle(
            GetPageOfDogOwnersQuery request,
            CancellationToken cancellationToken)
        {
            var userRepository = _unitOfWork.GetDogOwnerRepository();

            var page = await userRepository.GetPageOfDogOwnersAsync(request.Count, request.Page, cancellationToken);

            return Success(page.MapUserCollectionToPageODogOwnersDto());
        }
    };
};