namespace Doggo.Application.Requests.Commands.Message;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using MediatR;

public record DeleteMessageCommand(Guid MessageId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteMessageCommand, CommonResult>
    {
        private readonly IMessageRepository _messageRepository;

        public Handler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<CommonResult> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _messageRepository.GetAsync(request.MessageId, cancellationToken);

            if (message is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            _messageRepository.Remove(message);

            return Success();
        }
    }
}