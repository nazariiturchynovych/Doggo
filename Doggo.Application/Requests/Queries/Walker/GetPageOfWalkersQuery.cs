namespace Doggo.Application.Requests.Queries.Walker;

using Domain.DTO;
using Domain.DTO.Walker;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetWalkerDto>>> Handle(
            GetPageOfWalkersQuery request,
            CancellationToken cancellationToken)
        {
            var walkerRepository = _unitOfWork.GetWalkerRepository();

            var page = await walkerRepository.GetPageOfWalkersAsync(
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