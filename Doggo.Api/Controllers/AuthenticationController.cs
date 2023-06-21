namespace Doggo.Controllers;

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
    private readonly IUrlHelper _urlHelper;

    public AuthenticationController(IMediator mediator, IUrlHelper urlHelper)
    {
        _mediator = mediator;
        _urlHelper = urlHelper;
    }

    [HttpPost("SignUp/SendEmailConfirmationToken")]
    public async Task<IActionResult> SignUp(SignUpCommand command, CancellationToken cancellationToken)
    {
        var signUpResult = await _mediator.Send(command, cancellationToken);

        if (signUpResult.IsFailure)
            return Ok(signUpResult);

        return Ok(
            await _mediator.Send(
                new SendEmailConfirmationTokenCommand(
                    command.Email,
                    _urlHelper.Action(
                        "ConfirmEmail",
                        "Authentication",
                        null,
                        HttpContext.Request.Scheme)!),
                cancellationToken));
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
        SendResetPasswordTokenCommand sendResetPasswordTokenCommand,
        CancellationToken cancellationToken)
    {
        return Ok(
            await _mediator.Send(
                sendResetPasswordTokenCommand,
                cancellationToken));
    }

    [HttpPost("ConfirmResetPasswordCommand")]
    public async Task<IActionResult> ConfirmResetPasswordCommand(
        string userId,
        string newPassword,
        string token,
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