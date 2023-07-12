namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.Authentication.ChangePasswordCommand;
using Application.Requests.Commands.Authentication.ConfirmEmailAndSetDefaultRoleCommand;
using Application.Requests.Commands.Authentication.ConfirmResetPasswordCommand;
using Application.Requests.Commands.Authentication.FacebookSignUpCommand;
using Application.Requests.Commands.Authentication.GoogleSignUpCommand;
using Application.Requests.Commands.Authentication.SendEmailConfirmationTokenCommand;
using Application.Requests.Commands.Authentication.SignUpCommand;
using Application.Requests.Queries.Authentication.FacebookSignInQuery;
using Application.Requests.Queries.Authentication.GoogleSignInQuery;
using Application.Requests.Queries.Authentication.SignInQuery;
using Application.Responses.Authentication;
using Domain.Results;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("SignUp/SendEmailConfirmationToken")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUp(SignUpCommand command, CancellationToken cancellationToken)
    {
        var signUpResult = await _mediator.Send(command, cancellationToken);

        if (signUpResult.IsFailure)
            return (signUpResult).ToActionResult();

        return (await _mediator.Send(new SendEmailConfirmationTokenCommand(command.Email), cancellationToken)).ToActionResult();
    }

    [HttpPost("SendEmailVerificationToken")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendEmailVerificationToken(
        SendEmailConfirmationTokenCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }


    [HttpGet("ConfirmEmail")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmEmail(
        Guid userId,
        string token)
    {
        return (await _mediator.Send(new ConfirmEmailAndSetDefaultRoleCommand(userId, token))).ToActionResult();
    }

    [HttpPost("SignIn")]
    [ProducesResponseType(typeof(CommonResult<SignInResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignIn([FromBody] SignInQuery signInQuery)
    {
        return (await _mediator.Send(signInQuery)).ToActionResult();
    }

    [Authorize(Roles = "User, Admin")]
    [HttpPost("ChangePassword")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(
        ChangePasswordCommand changePasswordCommand,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(changePasswordCommand, cancellationToken)).ToActionResult();
    }

    [HttpPost("SendPasswordResetConfirmationToken")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendResetPasswordConfirmationToken(
        string token,
        Guid userId,
        string newPassword,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new ConfirmResetPasswordCommand(token, userId, newPassword), cancellationToken)).ToActionResult();
    }

    [HttpPost("ConfirmResetPasswordCommand")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmResetPasswordCommand(
        string token,
        Guid userId,
        string newPassword,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new ConfirmResetPasswordCommand(token, userId, newPassword), cancellationToken)).ToActionResult();
    }

    [HttpPost("SingUp/Google")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUpGoogle(GoogleSignUpCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpGet("SingIn/Google")]
    [ProducesResponseType(typeof(CommonResult<SignInResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignInGoogle(string credential, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GoogleSignInQuery(credential), cancellationToken)).ToActionResult();
    }

    [HttpPost("SingUp/Facebook")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUpFacebook(FacebookSignUpCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpGet("SingIn/Facebook")]
    [ProducesResponseType(typeof(CommonResult<SignInResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignInFacebook(string accessToken, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new FacebookSignInQuery(accessToken), cancellationToken)).ToActionResult();
    }
}