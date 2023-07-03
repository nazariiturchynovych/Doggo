namespace Doggo.Application.Requests.Commands.Chat;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;

public record UpdateChatCommand(Guid ChatId, string? Name) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<UpdateChatCommand, CommonResult>
    {
        private readonly IChatRepository _chatRepository;

        public Handler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<CommonResult> Handle(UpdateChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await _chatRepository.GetAsync(request.ChatId, cancellationToken);

            if (chat is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var updatedChat = request.MapUpdateChatCommandToChat(chat);

            _chatRepository.Update(updatedChat);

            return Success();
        }
    }
}