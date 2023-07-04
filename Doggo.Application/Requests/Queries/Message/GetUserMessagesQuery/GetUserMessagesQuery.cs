namespace Doggo.Application.Requests.Queries.Message.GetUserMessagesQuery;

using Domain.Results;
using DTO;
using DTO.Chat.Message;
using MediatR;

public record GetUserMessagesQuery(Guid UserId) : IRequest<CommonResult<PageOfTDataDto<GetMessageDto>>>;