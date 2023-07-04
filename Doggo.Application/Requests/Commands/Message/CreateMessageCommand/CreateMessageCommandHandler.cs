namespace Doggo.Application.Requests.Commands.Message.CreateMessageCommand;

using Abstractions.Persistence.Read;
using Domain.Entities.Chat;
using Domain.Results;
using MediatR;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, CommonResult>
{
    private readonly IMessageRepository _messageRepository;

    public CreateMessageCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<CommonResult> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        await _messageRepository.AddAsync(
            new Message
            {
                Value = request.Value,
                UserId = request.UserId,
                CreatedDate = DateTime.UtcNow,
                ChatId = request.ChatId
            });

        return Success();
    }
}