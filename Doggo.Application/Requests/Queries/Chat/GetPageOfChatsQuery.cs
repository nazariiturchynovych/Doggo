namespace Doggo.Application.Requests.Queries.Chat;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Chat;
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
        private readonly IChatRepository _chatRepository;


        public Handler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<CommonResult<PageOfTDataDto<ChatDto>>> Handle(
            GetPageOfChatsQuery request,
            CancellationToken cancellationToken)
        {
            var page = await _chatRepository.GetPageOfChatsAsync(
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