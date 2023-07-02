namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.Authentication;
using Application.Requests.Queries.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> SignUp(SignUpCommand command, CancellationToken cancellationToken)
    {
        var signUpResult = await _mediator.Send(command, cancellationToken);

        if (signUpResult.IsFailure)
            return Ok(signUpResult);

        return Ok(await _mediator.Send(new SendEmailConfirmationTokenCommand(command.Email), cancellationToken));
    }

    [HttpPost("SendEmailVerificationToken")]
    public async Task<IActionResult> SendEmailVerificationToken(
        SendEmailConfirmationTokenCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }


    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(
        Guid userId,
        string token)
    {
        return Ok(await _mediator.Send(new ConfirmEmailAndSetDefaultRoleCommand(userId, token)));
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn([FromBody] SignInQuery signInQuery)
    {
        return Ok(await _mediator.Send(signInQuery));
    }

    [Authorize(Roles = "User, Admin")]
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword(
        ChangePasswordCommand changePasswordCommand,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(changePasswordCommand, cancellationToken));
    }

    [HttpPost("SendPasswordResetConfirmationToken")]
    public async Task<IActionResult> SendResetPasswordConfirmationToken(
        string token,
        Guid userId,
        string newPassword,
        CancellationToken cancellationToken)
    {
        return Ok(
            await _mediator.Send(
                new ConfirmResetPasswordCommand(token, userId, newPassword),
                cancellationToken));
    }

    [HttpGet("ConfirmResetPasswordCommand")] // don't work with post method with link from email
    public async Task<IActionResult> ConfirmResetPasswordCommand(
        string token,
        Guid userId,
        string newPassword,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new ConfirmResetPasswordCommand(token, userId, newPassword), cancellationToken));
    }

    [HttpPost("SingUp/Google")]
    public async Task<IActionResult> SignUpGoogle(GoogleSignUpCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpGet("SingIn/Google")]
    public async Task<IActionResult> SignInGoogle(string credential, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GoogleSignInQuery(credential), cancellationToken));
    }

    [HttpPost("SingUp/Facebook")]
    public async Task<IActionResult> SignUpFacebook(FacebookSignUpCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpGet("SingIn/Facebook")]
    public async Task<IActionResult> SignInFacebook(string accessToken, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new FacebookSignInQuery(accessToken), cancellationToken));
    }
}