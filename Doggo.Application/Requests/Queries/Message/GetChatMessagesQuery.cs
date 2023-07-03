namespace Doggo.Application.Requests.Queries.Message;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Chat.Message;
using Mappers;
using MediatR;

public record GetChatMessagesQuery(
    Guid ChatId,
    int Count) : IRequest<CommonResult<PageOfTDataDto<GetMessageDto>>>
{
    public class Handler : IRequestHandler<GetChatMessagesQuery, CommonResult<PageOfTDataDto<GetMessageDto>>>
    {
        private readonly IMessageRepository _messageRepository;

        public Handler(IMessageRepository messageRepository)
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
}