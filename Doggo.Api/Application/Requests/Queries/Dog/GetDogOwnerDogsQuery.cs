namespace Doggo.Application.Requests.Queries.Dog;

using Domain.Constants.ErrorConstants;
using Domain.DTO;
using Domain.DTO.Dog;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetDogOwnerDogsQuery(Guid DogOwnerId) : IRequest<CommonResult<PageOfTDataDto<GetDogDto>>>
{
    public class Handler : IRequestHandler<GetDogOwnerDogsQuery, CommonResult<PageOfTDataDto<GetDogDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetDogDto>>> Handle(
            GetDogOwnerDogsQuery request,
            CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetDogRepository();

            var dogs = await repository.GetDogOwnerDogsAsync(request.DogOwnerId, cancellationToken);

            if (!dogs.Any())
                return Failure<PageOfTDataDto<GetDogDto>>(CommonErrors.EntityDoesNotExist);

            return Success(dogs.MapDogCollectionToPageOfDogDto());
        }
    }
};