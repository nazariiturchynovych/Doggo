namespace Doggo.Application.Requests.Queries.Chat.GetChatByIdQuery;

using Abstractions.Repositories;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.Chat;

public class GetChatByIdQueryHandler : IRequestHandler<GetChatByIdQuery, CommonResult<ChatResponse>>
{
    private readonly IChatRepository _chatRepository;

    public GetChatByIdQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<CommonResult<ChatResponse>> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetWithMessages(request.Id, cancellationToken);

        if (chat is null)
            return Failure<ChatResponse>(CommonErrors.EntityDoesNotExist);

        var entityDto = chat.MapChatToChatResponse();

        return Success(entityDto);
    }
}