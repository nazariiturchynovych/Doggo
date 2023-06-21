namespace Doggo.Application.Requests.Queries.DogOwner;

using Domain.DTO;
using Domain.DTO.DogOwner;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetPageOfDogOwnersQuery(
    string? NameSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Count,
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
                request.Count,
                request.PageCount,
                cancellationToken);

            return Success(page.MapDogOwnerCollectionToPageODogOwnersDto());
        }
    };
};