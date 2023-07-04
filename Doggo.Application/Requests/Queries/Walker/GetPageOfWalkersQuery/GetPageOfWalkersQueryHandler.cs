namespace Doggo.Application.Requests.Queries.Walker.GetPageOfWalkersQuery;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Walker;
using Mappers;
using MediatR;

public class GetPageOfWalkersQueryHandler : IRequestHandler<GetPageOfWalkersQuery, CommonResult<PageOfTDataDto<GetWalkerDto>>>
{
    private readonly IWalkerRepository _walkerRepository;


    public GetPageOfWalkersQueryHandler(IWalkerRepository walkerRepository)
    {
        _walkerRepository = walkerRepository;
    }

    public async Task<CommonResult<PageOfTDataDto<GetWalkerDto>>> Handle(
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

        return Success(page.MapWalkerCollectionToPageOWalkersDto());
    }
}