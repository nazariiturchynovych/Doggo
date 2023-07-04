namespace Doggo.Application.Requests.Queries.Chat.GetChatByIdQuery;

using Domain.Results;
using DTO.Chat;
using MediatR;

public record GetChatByIdQuery(Guid Id) : IRequest<CommonResult<GetChatDto>>;