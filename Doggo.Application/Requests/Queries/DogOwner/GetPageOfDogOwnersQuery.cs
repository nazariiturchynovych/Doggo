namespace Doggo.Application.Requests.Queries.DogOwner;

using Domain.Results;
using DTO;
using DTO.DogOwner;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetPageOfDogOwnersQuery(
    string? NameSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOfTDataDto<GetDogOwnerDto>>>
{
    public class Handler : IRequestHandler<GetPageOfDogOwnersQuery, CommonResult<PageOfTDataDto<GetDogOwnerDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetDogOwnerDto>>> Handle(
            GetPageOfDogOwnersQuery request,
            CancellationToken cancellationToken)
        {
            var dogOwnerRepository = _unitOfWork.GetDogOwnerRepository();

            var page = await dogOwnerRepository
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
};