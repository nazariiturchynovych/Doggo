namespace Doggo.Application.Requests.Queries.Walker;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Walker;
using Mappers;
using MediatR;

public record GetPageOfWalkersQuery(
    string? NameSearchTerm,
    string? SkillSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount
) : IRequest<CommonResult<PageOfTDataDto<GetWalkerDto>>>
{
    public class Handler : IRequestHandler<GetPageOfWalkersQuery, CommonResult<PageOfTDataDto<GetWalkerDto>>>
    {
        private readonly IWalkerRepository _walkerRepository;


        public Handler(IWalkerRepository walkerRepository)
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
}