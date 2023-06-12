namespace Doggo.Application.Requests.Queries.User;

using Domain.DTO;
using Domain.Results.Abstract;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetPageOfUsersQuery(int Count, int Page) : IRequest<ICommonResult<PageOfTDataDto<GetUserDto>>>
{
    public class Handler : IRequestHandler<GetPageOfUsersQuery, ICommonResult<PageOfTDataDto<GetUserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommonResult<PageOfTDataDto<GetUserDto>>> Handle(
            GetPageOfUsersQuery request,
            CancellationToken cancellationToken)
        {
            var userRepository = _unitOfWork.GetUserRepository();

            var page = await userRepository.GetPageOfUsersAsync(request.Count, request.Page, cancellationToken);

            return Success(page.MapUserCollectionToPageOfUsersDto());
        }
    };
};