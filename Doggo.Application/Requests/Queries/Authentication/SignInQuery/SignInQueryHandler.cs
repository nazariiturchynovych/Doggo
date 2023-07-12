namespace Doggo.Application.Requests.Queries.Authentication.SignInQuery;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Responses.Authentication;

public class SignInQueryHandler : IRequestHandler<SignInQuery, CommonResult<SignInResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
    private readonly IUserRepository _userRepository;


    public SignInQueryHandler(
        UserManager<User> userManager,
        IJwtTokenGeneratorService jwtTokenGeneratorService,
        IUserRepository userRepository)
    {
        _userManager = userManager;
        _jwtTokenGeneratorService = jwtTokenGeneratorService;
        _userRepository = userRepository;
    }

    public async Task<CommonResult<SignInResponse>> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithRoles(request.Email, cancellationToken);

        if (user is null)
            return Failure<SignInResponse>(CommonErrors.EntityDoesNotExist);

        var logInResult = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!logInResult)
            return Failure<SignInResponse>(UserErrors.UserDoesNotExist);

        if (!user.IsApproved)
            return Failure<SignInResponse>(UserErrors.UserIsNotApproved);

        return Success(new SignInResponse(_jwtTokenGeneratorService.GenerateToken(user)));
    }
}