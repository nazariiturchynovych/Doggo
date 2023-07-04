namespace Doggo.Application.Requests.Queries.Chat.GetChatByIdQuery;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO.Chat;
using Mappers;
using MediatR;

public class GetChatByIdQueryHandler : IRequestHandler<GetChatByIdQuery, CommonResult<GetChatDto>>
{
    private readonly IChatRepository _chatRepository;

    public GetChatByIdQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<CommonResult<GetChatDto>> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetWithMessages(request.Id, cancellationToken);

        if (chat is null)
            return Failure<GetChatDto>(CommonErrors.EntityDoesNotExist);

        var entityDto = chat.MapChatToGetChatDto();

        return Success(entityDto);
    }
}