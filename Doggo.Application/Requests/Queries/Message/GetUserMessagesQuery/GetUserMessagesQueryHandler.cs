namespace Doggo.Application.Requests.Queries.Message.GetUserMessagesQuery;

using Abstractions.Repositories;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.Chat.Message;

public class GetUserMessagesQueryHandler : IRequestHandler<GetUserMessagesQuery, CommonResult<List<MessageResponse>>>
{
    private readonly IMessageRepository _messageRepository;

    public GetUserMessagesQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<CommonResult<List<MessageResponse>>> Handle(
        GetUserMessagesQuery request,
        CancellationToken cancellationToken)
    {

        var dogs = await _messageRepository.GetUserMessagesAsync(request.UserId, cancellationToken);

        if (dogs is null || !dogs.Any())
            return Failure<List<MessageResponse>>(CommonErrors.EntityDoesNotExist);

        return Success(dogs.MapMessageCollectionToListOfMessageResponse());
    }
}