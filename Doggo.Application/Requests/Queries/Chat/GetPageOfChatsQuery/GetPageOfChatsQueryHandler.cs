namespace Doggo.Application.Requests.Queries.Chat.GetPageOfChatsQuery;

using Abstractions.Repositories;
using Domain.Results;
using Mappers;
using MediatR;
using Responses;
using Responses.Chat;

public class GetPageOfChatsQueryHandler : IRequestHandler<GetPageOfChatsQuery, CommonResult<PageOf<ChatResponse>>>
{
    private readonly IChatRepository _chatRepository;


    public GetPageOfChatsQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<CommonResult<PageOf<ChatResponse>>> Handle(
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

        return Success(page.MapChatCollectionToPageOfChatResponse());
    }
};