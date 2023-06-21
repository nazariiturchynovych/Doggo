namespace Doggo.Application.Requests.Queries.Dog;

using Domain.DTO;
using Domain.DTO.Dog;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetPageOfDogsQuery(
    string? NameSearchTerm,
    string? DescriptionSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Count,
    int PageCount) : IRequest<CommonResult<PageOfTDataDto<GetDogDto>>>
{
    public class Handler : IRequestHandler<GetPageOfDogsQuery, CommonResult<PageOfTDataDto<GetDogDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetDogDto>>> Handle(
            GetPageOfDogsQuery request,
            CancellationToken cancellationToken)
        {
            var dogRepository = _unitOfWork.GetDogRepository();

            var page = await dogRepository.GetPageOfDogsAsync(
                request.NameSearchTerm,
                request.DescriptionSearchTerm,
                request.SortColumn,
                request.SortOrder,
                request.Count,
                request.PageCount,
                cancellationToken);

            return Success(page.MapDogCollectionToPageOfDogDto());
        }
    };
};