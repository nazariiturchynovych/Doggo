namespace Doggo.Application.Requests.Commands.Authentication.SignUpCommand;

using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, CommonResult<Guid>>
{
    private readonly UserManager<User> _userManager;

    public SignUpCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CommonResult<Guid>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is not null)
        {
            return Failure<Guid>(CommonErrors.EntityAlreadyExist);
        }

        var userToAdd = new User
        {
            Email = request.Email,
            UserName = request.Email,
            FirstName = "",
            LastName = ""
        };

        var result = await _userManager.CreateAsync(userToAdd, request.Password);

        if (!result.Succeeded)
            return Failure<Guid>(UserErrors.UserCreateFailed);

        return Success(user!.Id);
    }
}