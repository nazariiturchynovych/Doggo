namespace Doggo.Application.Requests.Queries.Chat.GetPageOfChatsQuery;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Chat;
using Mappers;
using MediatR;

public class GetPageOfChatsQueryHandler : IRequestHandler<GetPageOfChatsQuery, CommonResult<PageOfTDataDto<ChatDto>>>
{
    private readonly IChatRepository _chatRepository;


    public GetPageOfChatsQueryHandler(IChatRepository chatRepository)
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