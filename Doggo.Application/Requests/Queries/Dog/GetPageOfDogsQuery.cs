namespace Doggo.Application.Requests.Queries.Dog;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Dog;
using Mappers;
using MediatR;

public record GetPageOfDogsQuery(
    string? NameSearchTerm,
    string? DescriptionSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOfTDataDto<GetDogDto>>>
{
    public class Handler : IRequestHandler<GetPageOfDogsQuery, CommonResult<PageOfTDataDto<GetDogDto>>>
    {
        private readonly IDogRepository _dogRepository;


        public Handler(IDogRepository dogRepository)
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
};