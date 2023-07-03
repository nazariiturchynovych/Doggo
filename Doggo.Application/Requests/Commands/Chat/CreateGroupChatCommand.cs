namespace Doggo.Application.Requests.Commands.Chat;

using Domain.Entities.Chat;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CurrentUserService;
using MediatR;

public record CreateGroupChatCommand(
    string Name,
    List<Guid> UserIds) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreateGroupChatCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public Handler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<CommonResult> Handle(CreateGroupChatCommand request, CancellationToken cancellationToken)
        {
            var chatRepository = _unitOfWork.GetChatRepository();

            var chat = new Chat
            {
                Name = request.Name,
            };

            await chatRepository.AddAsync(chat);

            var userRepository = _unitOfWork.GetUserRepository();

            var validUsers = new List<Guid>();

            foreach (var userId in request.UserIds)
            {
                var user = await userRepository.GetWithPersonalIdentifierAsync(userId, cancellationToken);
                if (user is not null)
                    validUsers.Add(userId);
            }

            var userChats = validUsers.Select(
                    userId => new UserChat
                    {
                        ChatId = chat.Id,
                        UserId = userId
                    })
                .ToList();

            userChats.Add(
                new UserChat
                {
                    ChatId = chat.Id,
                    UserId = _currentUserService.GetUserId()
                });

            var userChatRepository = _unitOfWork.GetUserChatRepository();

            await userChatRepository.AddRangeAsync(userChats);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}