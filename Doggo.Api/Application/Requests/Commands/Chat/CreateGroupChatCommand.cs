namespace Doggo.Application.Requests.Commands.Chat;

using Domain.Entities.Chat;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;

public record CreateGroupChatCommand(
    string Name,
    Guid ChatId,
    ICollection<Guid> UsersId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreateGroupChatCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(CreateGroupChatCommand request, CancellationToken cancellationToken)
        {
            var chatRepository = _unitOfWork.GetChatRepository();

            var chat = new Chat()
            {
                Name = request.Name,
            };

            await chatRepository.AddAsync(chat);

            var userRepository = _unitOfWork.GetUserRepository();

            var validUsers = new List<Guid>();

            foreach (var userId in request.UsersId)
            {
                var user = await userRepository.GetAsync(userId, cancellationToken);
                if (user is not null)
                    validUsers.Add(userId);
            }

            var userChats = validUsers.Select(
                userId => new UserChat()
                {
                    ChatId = request.ChatId,
                    UserId = userId
                });

            var userChatRepository = _unitOfWork.GetUserChatRepository();

            await userChatRepository.AddRangeAsync(userChats);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}