namespace Doggo.Application.Requests.Queries.Message.GetChatMessagesQuery;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Chat.Message;
using Mappers;
using MediatR;

public class GetChatMessagesQueryHandler : IRequestHandler<GetChatMessagesQuery, CommonResult<PageOfTDataDto<GetMessageDto>>>
{
    private readonly IMessageRepository _messageRepository;

    public GetChatMessagesQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<CommonResult<PageOfTDataDto<GetMessageDto>>> Handle(
        GetChatMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var page = await _messageRepository.GetChatMessagesAsync(
            request.ChatId,
            request.Count,
            cancellationToken);

        return Success(page.MapMessageCollectionToPageOfMessageDto());
    }
}