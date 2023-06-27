namespace Doggo.Application.Requests.Commands.Chat;

using Domain.Entities.Chat;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;

public record CreatePrivateChatCommand(
    string Name,
    Guid FirstUserId,
    Guid SecondUserId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreatePrivateChatCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(CreatePrivateChatCommand request, CancellationToken cancellationToken)
        {
            var chatRepository = _unitOfWork.GetChatRepository();

            var chat = new Chat()
            {
                Name = request.Name,
                IsPrivate = true
            };

            await chatRepository.AddAsync(chat);

            var userChatRepository = _unitOfWork.GetUserChatRepository();

            var userChats = new List<UserChat>()
            {
                new()
                {
                    ChatId = chat.Id,
                    UserId = request.FirstUserId
                },
                new()
                {
                    ChatId = chat.Id,
                    UserId = request.SecondUserId
                }
            };

            await userChatRepository.AddRangeAsync(userChats);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}