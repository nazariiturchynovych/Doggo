namespace Doggo.Application.Requests.Commands.Message.UpdateMessageCommand;

using Abstractions.Repositories;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;

public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, CommonResult>
{
    private readonly IMessageRepository _messageRepository;

    public UpdateMessageCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<CommonResult> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        var currentMessage = await _messageRepository.GetAsync(request.MessageId, cancellationToken);

        if (currentMessage is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        var updatedMessage = request.MapMessageUpdateCommandToMessage(currentMessage);

        _messageRepository.Update(updatedMessage);

        return Success();
    }
}