namespace Doggo.Application.Requests.Queries.DogOwner.GetPageOfDogOwnersQuery;

using Abstractions.Persistence.Read;
using Abstractions.Repositories;
using Domain.Results;
using DTO;
using DTO.DogOwner;
using Mappers;
using MediatR;

public class GetPageOfDogOwnersQueryHandler : IRequestHandler<GetPageOfDogOwnersQuery, CommonResult<PageOfTDataDto<GetDogOwnerDto>>>
{
    private readonly IDogOwnerRepository _dogOwnerRepository;


    public GetPageOfDogOwnersQueryHandler(IDogOwnerRepository dogOwnerRepository)
    {
        _dogOwnerRepository = dogOwnerRepository;
    }

    public async Task<CommonResult<PageOfTDataDto<GetDogOwnerDto>>> Handle(
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

        return Success(page.MapDogOwnerCollectionToPageODogOwnersDto());
    }
};