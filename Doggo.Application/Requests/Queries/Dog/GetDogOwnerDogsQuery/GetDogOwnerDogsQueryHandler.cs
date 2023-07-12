namespace Doggo.Application.Requests.Queries.Dog.GetDogOwnerDogsQuery;

using Abstractions.Repositories;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.Dog;

public class GetDogOwnerDogsQueryHandler : IRequestHandler<GetDogOwnerDogsQuery, CommonResult<List<DogResponse>>>
{
    private readonly IDogRepository _dogRepository;


    public GetDogOwnerDogsQueryHandler(IDogRepository dogRepository)
    {
        _dogRepository = dogRepository;
    }

    public async Task<CommonResult<List<DogResponse>>> Handle(
        GetDogOwnerDogsQuery request,
        CancellationToken cancellationToken)
    {

        var dogs = await _dogRepository.GetDogOwnerDogsAsync(request.DogOwnerId, cancellationToken);

        if (!dogs.Any())
            return Failure<List<DogResponse>>(CommonErrors.EntityDoesNotExist);

        return Success(dogs.MapDogCollectionToListOfDogResponse());
    }
}