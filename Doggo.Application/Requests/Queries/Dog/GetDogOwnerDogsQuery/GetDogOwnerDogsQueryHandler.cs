namespace Doggo.Application.Requests.Queries.Dog.GetDogOwnerDogsQuery;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO;
using DTO.Dog;
using Mappers;
using MediatR;

public class GetDogOwnerDogsQueryHandler : IRequestHandler<GetDogOwnerDogsQuery, CommonResult<PageOfTDataDto<GetDogDto>>>
{
    private readonly IDogRepository _dogRepository;


    public GetDogOwnerDogsQueryHandler(IDogRepository dogRepository)
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