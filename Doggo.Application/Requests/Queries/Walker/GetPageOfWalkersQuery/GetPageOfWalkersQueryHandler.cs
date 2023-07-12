namespace Doggo.Application.Requests.Queries.Walker.GetPageOfWalkersQuery;

using Abstractions.Repositories;
using Domain.Results;
using Mappers;
using MediatR;
using Responses;
using Responses.Walker;

public class GetPageOfWalkersQueryHandler : IRequestHandler<GetPageOfWalkersQuery, CommonResult<PageOf<WalkerResponse>>>
{
    private readonly IWalkerRepository _walkerRepository;


    public GetPageOfWalkersQueryHandler(IWalkerRepository walkerRepository)
    {
        _walkerRepository = walkerRepository;
    }

    public async Task<CommonResult<PageOf<WalkerResponse>>> Handle(
        GetPageOfWalkersQuery request,
        CancellationToken cancellationToken)
    {

        var page = await _walkerRepository.GetPageOfWalkersAsync(
            request.NameSearchTerm,
            request.SkillSearchTerm,
            request.SortColumn,
            request.SortOrder,
            request.PageCount,
            request.Page,
            cancellationToken);

        return Success(page.MapWalkerCollectionToPageOWalkersResponse());
    }
}