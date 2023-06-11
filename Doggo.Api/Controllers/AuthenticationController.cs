namespace Doggo.Controllers;

using Application.Requests.Commands.User;
using Application.Requests.Queries.User;
using MediatR;
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
        {
            return Ok();
        }

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
        int userId,
        string token)
    {
        return Ok(await _mediator.Send(new ConfirmEmailAndSetDefaultRoleCommand(userId, token)));
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(SignInQuery signInQuery, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(signInQuery, cancellationToken));
    }

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
        ConfirmResetPasswordCommand confirmResetPasswordCommand,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(confirmResetPasswordCommand, cancellationToken));
    }

}