namespace Doggo.Application.Requests.Queries.User.GetPageOfUsersQuery;

using Abstractions.Repositories;
using Domain.Results;
using Mappers;
using MediatR;
using Responses;
using Responses.User;

public class GetPageOfUsersQueryHandler : IRequestHandler<GetPageOfUsersQuery, CommonResult<PageOf<UserResponse>>>
{
    private readonly IUserRepository _userRepository;

    public GetPageOfUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<CommonResult<PageOf<UserResponse>>> Handle(
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

        return Success(page.MapUserCollectionToPageOfUsersResponse());
    }
};