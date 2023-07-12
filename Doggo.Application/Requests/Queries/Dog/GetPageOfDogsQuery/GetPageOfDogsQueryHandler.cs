namespace Doggo.Application.Requests.Queries.Dog.GetPageOfDogsQuery;

using Abstractions.Repositories;
using Domain.Results;
using Mappers;
using MediatR;
using Responses;
using Responses.Dog;

public class GetPageOfDogsQueryHandler : IRequestHandler<GetPageOfDogsQuery, CommonResult<PageOf<DogResponse>>>
{
    private readonly IDogRepository _dogRepository;

    public GetPageOfDogsQueryHandler(IDogRepository dogRepository)
    {
        _dogRepository = dogRepository;
    }

    public async Task<CommonResult<PageOf<DogResponse>>> Handle(
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

        return Success(page.MapDogCollectionToPageOfDogResponse());
    }
};