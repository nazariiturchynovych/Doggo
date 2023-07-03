namespace Doggo.Application.Requests.Commands.Message;

using Abstractions.Persistence.Read;
using Domain.Entities.Chat;
using Domain.Results;
using MediatR;

public record CreateMessageCommand(Guid UserId, Guid ChatId, string Value) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreateMessageCommand, CommonResult>
    {
        private readonly IMessageRepository _messageRepository;

        public Handler(IMessageRepository messageRepository)
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
}