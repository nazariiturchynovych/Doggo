namespace Doggo.Application.Requests.Queries.Message.GetChatMessagesQuery;

using Domain.Results;
using DTO;
using DTO.Chat.Message;
using MediatR;

public record GetChatMessagesQuery(
    Guid ChatId,
    int Count) : IRequest<CommonResult<PageOfTDataDto<GetMessageDto>>>;