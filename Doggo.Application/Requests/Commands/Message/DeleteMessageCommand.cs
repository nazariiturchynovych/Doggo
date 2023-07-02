namespace Doggo.Api.Application.Requests.Commands.Message;

using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;

public record DeleteMessageCommand(Guid MessageId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteMessageCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetMessageRepository();

            var message = await repository.GetAsync(request.MessageId, cancellationToken);

            if (message is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            repository.Remove(message);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}