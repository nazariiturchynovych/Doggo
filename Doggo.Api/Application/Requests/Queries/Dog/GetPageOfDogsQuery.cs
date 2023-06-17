namespace Doggo.Application.Requests.Queries.Dog;

using Domain.DTO;
using Domain.DTO.Dog;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetPageOfDogsQuery(int Count, int Page) : IRequest<CommonResult<PageOfTDataDto<GetDogDto>>>
{
    public class Handler : IRequestHandler<GetPageOfDogsQuery, CommonResult<PageOfTDataDto<GetDogDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetDogDto>>> Handle(
            GetPageOfDogsQuery request,
            CancellationToken cancellationToken)
        {
            var userRepository = _unitOfWork.GetDogRepository();

            var page = await userRepository.GetPageOfDogsAsync(request.Count, request.Page, cancellationToken);

            return Success(page.MapUserCollectionToPageODogDto());
        }
    };
};