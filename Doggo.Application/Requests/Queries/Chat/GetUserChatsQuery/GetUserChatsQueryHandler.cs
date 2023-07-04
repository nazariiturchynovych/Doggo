namespace Doggo.Application.Requests.Queries.Chat.GetUserChatsQuery;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO;
using DTO.Chat;
using Mappers;
using MediatR;

public class GetUserChatsQueryHandler : IRequestHandler<GetUserChatsQuery, CommonResult<PageOfTDataDto<ChatDto>>>
{
    private readonly IChatRepository _chatRepository;


    public GetUserChatsQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<CommonResult<PageOfTDataDto<ChatDto>>> Handle(
        GetUserChatsQuery request,
        CancellationToken cancellationToken)
    {

        var chats = await _chatRepository.GetUserChatsAsync(request.ChatOwnerId, cancellationToken);

        if (chats is null || !chats.Any())
            return Failure<PageOfTDataDto<ChatDto>>(CommonErrors.EntityDoesNotExist);

        return Success(chats.MapChatCollectionToPageOfChatDto());
    }
}