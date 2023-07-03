namespace Doggo.Application.Requests.Queries.DogOwner;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.DogOwner;
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
        private readonly IDogOwnerRepository _dogOwnerRepository;


        public Handler(IDogOwnerRepository dogOwnerRepository)
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
};