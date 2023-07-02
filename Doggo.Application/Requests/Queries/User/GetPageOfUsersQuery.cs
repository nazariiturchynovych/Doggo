namespace Doggo.Application.Requests.Queries.User;

using Domain.DTO;
using Domain.DTO.User;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetPageOfUsersQuery(
    string? NameSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOfTDataDto<GetUserDto>>>
{
    public class Handler : IRequestHandler<GetPageOfUsersQuery, CommonResult<PageOfTDataDto<GetUserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetUserDto>>> Handle(
            GetPageOfUsersQuery request,
            CancellationToken cancellationToken)
        {
            var userRepository = _unitOfWork.GetUserRepository();

            var page = await userRepository.GetPageOfUsersAsync(
                request.NameSearchTerm,
                request.SortColumn,
                request.SortOrder,
                request.PageCount,
                request.Page,
                cancellationToken);

            return Success(page.MapUserCollectionToPageOfUsersDto());
        }
    };
};