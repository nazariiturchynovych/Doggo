namespace Doggo.Application.Requests.Commands.Message;

using Domain.Entities.Chat;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;

public record CreateMessageCommand(Guid UserId,Guid ChatId,string Value) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreateMessageCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetMessageRepository();

            await repository.AddAsync(
                new Message
                {
                    Value = request.Value,
                    UserId = request.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ChatId = request.ChatId
                });


            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}