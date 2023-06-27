namespace Doggo.Application.Requests.Queries.Chat;

using Domain.Constants.ErrorConstants;
using Domain.DTO.Chat;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetChatByIdQuery(Guid Id) : IRequest<CommonResult<GetChatDto>>
{
    public class Handler : IRequestHandler<GetChatByIdQuery, CommonResult<GetChatDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<GetChatDto>> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetChatRepository();

            var chat = await repository.GetWithMessages(request.Id, cancellationToken);

            if (chat is null)
                return Failure<GetChatDto>(CommonErrors.EntityDoesNotExist);

            var entityDto = chat.MapChatToGetChatDto();

            return Success(entityDto);
        }
    }
};