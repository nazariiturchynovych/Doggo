namespace Doggo.Application.Requests.Queries.DogOwner.GetPageOfDogOwnersQuery;

using Abstractions.Repositories;
using Domain.Results;
using Mappers;
using MediatR;
using Responses;
using Responses.DogOwner;

public class GetPageOfDogOwnersQueryHandler : IRequestHandler<GetPageOfDogOwnersQuery, CommonResult<PageOf<DogOwnerResponse>>>
{
    private readonly IDogOwnerRepository _dogOwnerRepository;


    public GetPageOfDogOwnersQueryHandler(IDogOwnerRepository dogOwnerRepository)
    {
        _dogOwnerRepository = dogOwnerRepository;
    }

    public async Task<CommonResult<PageOf<DogOwnerResponse>>> Handle(
        GetPageOfDogOwnersQuery request,
        CancellationToken cancellationToken)
    {

        var page = await _dogOwnerRepository
            .GetPageOfDogOwnersAsync(
                request.NameSearchTerm,
                request.SortColumn,
                request.SortOrder,
                request.Page,
                request.PageCount,
                cancellationToken);

        return Success(page.MapDogOwnerCollectionToPageOfDogOwnersResponse());
    }
};