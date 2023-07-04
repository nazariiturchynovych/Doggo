namespace Doggo.Application.Requests.Queries.Dog.GetPageOfDogsQuery;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Dog;
using Mappers;
using MediatR;

public class GetPageOfDogsQueryHandler : IRequestHandler<GetPageOfDogsQuery, CommonResult<PageOfTDataDto<GetDogDto>>>
{
    private readonly IDogRepository _dogRepository;

    public GetPageOfDogsQueryHandler(IDogRepository dogRepository)
    {
        _dogRepository = dogRepository;
    }

    public async Task<CommonResult<PageOfTDataDto<GetDogDto>>> Handle(
        GetPageOfDogsQuery request,
        CancellationToken cancellationToken)
    {
        var page = await _dogRepository.GetPageOfDogsAsync(
            request.NameSearchTerm,
            request.DescriptionSearchTerm,
            request.SortColumn,
            request.SortOrder,
            request.PageCount,
            request.Page,
            cancellationToken);

        return Success(page.MapDogCollectionToPageOfDogDto());
    }
};