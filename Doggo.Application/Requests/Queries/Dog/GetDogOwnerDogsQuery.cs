namespace Doggo.Application.Requests.Queries.Dog;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO;
using DTO.Dog;
using Mappers;
using MediatR;

public record GetDogOwnerDogsQuery(Guid DogOwnerId) : IRequest<CommonResult<PageOfTDataDto<GetDogDto>>>
{
    public class Handler : IRequestHandler<GetDogOwnerDogsQuery, CommonResult<PageOfTDataDto<GetDogDto>>>
    {
        private readonly IDogRepository _dogRepository;


        public Handler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public async Task<CommonResult<PageOfTDataDto<GetDogDto>>> Handle(
            GetDogOwnerDogsQuery request,
            CancellationToken cancellationToken)
        {

            var dogs = await _dogRepository.GetDogOwnerDogsAsync(request.DogOwnerId, cancellationToken);

            if (!dogs.Any())
                return Failure<PageOfTDataDto<GetDogDto>>(CommonErrors.EntityDoesNotExist);

            return Success(dogs.MapDogCollectionToPageOfDogDto());
        }
    }
};