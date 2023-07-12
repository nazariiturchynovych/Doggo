namespace Doggo.Application.Requests.Queries.Message.GetChatMessagesQuery;

using Abstractions.Repositories;
using Domain.Results;
using Mappers;
using MediatR;
using Responses;
using Responses.Chat.Message;

public class GetChatMessagesQueryHandler : IRequestHandler<GetChatMessagesQuery, CommonResult<List<MessageResponse>>>
{
    private readonly IMessageRepository _messageRepository;

    public GetChatMessagesQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<CommonResult<List<MessageResponse>>> Handle(
        GetChatMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var page = await _messageRepository.GetChatMessagesAsync(
            request.ChatId,
            request.Count,
            cancellationToken);

        return Success(page.MapMessageCollectionToListOfMessageResponse());
    }
}