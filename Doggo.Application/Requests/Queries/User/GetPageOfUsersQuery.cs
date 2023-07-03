namespace Doggo.Application.Requests.Queries.User;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.User;
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
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CommonResult<PageOfTDataDto<GetUserDto>>> Handle(
            GetPageOfUsersQuery request,
            CancellationToken cancellationToken)
        {
            var page = await _userRepository.GetPageOfUsersAsync(
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