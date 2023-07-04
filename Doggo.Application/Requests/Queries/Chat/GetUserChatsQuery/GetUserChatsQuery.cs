namespace Doggo.Application.Requests.Queries.Chat.GetUserChatsQuery;

using Domain.Results;
using DTO;
using DTO.Chat;
using MediatR;

public record GetUserChatsQuery(Guid ChatOwnerId) : IRequest<CommonResult<PageOfTDataDto<ChatDto>>>;