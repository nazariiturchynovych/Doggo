namespace Doggo.Application.Requests.Queries.Chat;

using Api.Application.Mappers;
using Domain.DTO;
using Domain.DTO.Chat;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetPageOfChatsQuery(
    string? NameSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOfTDataDto<ChatDto>>>
{
    public class Handler : IRequestHandler<GetPageOfChatsQuery, CommonResult<PageOfTDataDto<ChatDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<ChatDto>>> Handle(
            GetPageOfChatsQuery request,
            CancellationToken cancellationToken)
        {
            var dogRepository = _unitOfWork.GetChatRepository();

            var page = await dogRepository.GetPageOfChatsAsync(
                request.NameSearchTerm,
                request.SortColumn,
                request.SortOrder,
                request.PageCount,
                request.Page,
                cancellationToken);

            return Success(page.MapChatCollectionToPageOfChatDto());
        }
    };
};