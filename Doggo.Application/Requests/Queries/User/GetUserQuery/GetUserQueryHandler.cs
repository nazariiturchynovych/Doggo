namespace Doggo.Application.Requests.Queries.User.GetUserQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Abstractions.SqlConnectionFactory;
using Dapper;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.DogOwner;
using Domain.Entities.User;
using Domain.Entities.User.Documents;
using Domain.Entities.Walker;
using Domain.Results;
using Mappers;
using MediatR;
using Responses.User;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, CommonResult<UserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICacheService _cacheService;
    private readonly IDbConnectionFactory _connectionFactory;

    public GetUserQueryHandler(IUserRepository userRepository, ICacheService cacheService, IDbConnectionFactory connectionFactory)
    {
        _userRepository = userRepository;
        _cacheService = cacheService;
        _connectionFactory = connectionFactory;
    }

    public async Task<CommonResult<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var cachedUser = await _cacheService.GetData<User>(CacheKeys.User + request.Id, cancellationToken);

        if (cachedUser is null)
        {
            using var connection = _connectionFactory.CreateNpgSqlConnection();

            //TODO add custom propertyMapper, for not writing aliases myself


            var user = await connection.QueryAsync<User, PersonalIdentifier, Walker, DogOwner, User>(
                $@"
        SELECT
            u.first_name AS FirstName,
            u.last_name AS LastName,
            u.age AS Age,
            u.id AS Id,
            u.email AS Email,
            pi.id AS Id,
            pi.*,
            w.id AS Id,
            dow.id AS Id
        FROM users AS u
        LEFT JOIN personal_identifiers AS pi ON u.id = pi.user_id
        LEFT JOIN walkers AS w ON u.id = w.user_id
        LEFT JOIN dog_owners AS dow ON u.id = dow.user_id
        WHERE u.Id = @UserId
    ",
                (user, personalIdentifier, walker, dogOwner) =>
                {
                    user.PersonalIdentifier = personalIdentifier;
                    user.Walker = walker;
                    user.DogOwner = dogOwner;
                    return user;
                },
                new { UserId = request.Id },
                splitOn: "Id"
            );

            if (user.Any(x => false))
                return Failure<UserResponse>(CommonErrors.EntityDoesNotExist);

            await _cacheService.SetData(CacheKeys.User + user.First().Id, user.First(), cancellationToken);

            cachedUser = user.First();
        }

        var getUserDto = cachedUser.MapUserToUserResponse();

        return Success(getUserDto);
    }
}