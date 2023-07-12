namespace Doggo.Application.Requests.Queries.Chat.GetUserChatsQuery;

using Abstractions.Repositories;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.Chat;

public class GetUserChatsQueryHandler : IRequestHandler<GetUserChatsQuery, CommonResult<List<ChatResponse>>>
{
    private readonly IChatRepository _chatRepository;


    public GetUserChatsQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<CommonResult<List<ChatResponse>>> Handle(
        GetUserChatsQuery request,
        CancellationToken cancellationToken)
    {

        var chats = await _chatRepository.GetUserChatsAsync(request.ChatOwnerId, cancellationToken);

        if (chats is null || !chats.Any())
            return Failure<List<ChatResponse>>(CommonErrors.EntityDoesNotExist);

        foreach (var chat in chats)
        {
            var a = chat.MapChatToChatResponse();
        }

        return Success(chats.MapChatCollectionToListOfChatResponse());
    }
}