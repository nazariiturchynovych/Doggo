namespace Doggo.Application.Requests.Queries.Message.GetUserMessagesQuery;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO;
using DTO.Chat.Message;
using Mappers;
using MediatR;

public class GetUserMessagesQueryHandler : IRequestHandler<GetUserMessagesQuery, CommonResult<PageOfTDataDto<GetMessageDto>>>
{
    private readonly IMessageRepository _messageRepository;

    public GetUserMessagesQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<CommonResult<PageOfTDataDto<GetMessageDto>>> Handle(
        GetUserMessagesQuery request,
        CancellationToken cancellationToken)
    {

        var dogs = await _messageRepository.GetUserMessagesAsync(request.UserId, cancellationToken);

        if (dogs is null || !dogs.Any())
            return Failure<PageOfTDataDto<GetMessageDto>>(CommonErrors.EntityDoesNotExist);

        return Success(dogs.MapMessageCollectionToPageOfMessageDto());
    }
}