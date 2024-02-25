namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.Authentication.ConfirmEmailAndSetDefaultRoleCommand;
using Application.Requests.Commands.Authentication.ConfirmResetPasswordCommand;
using Application.Requests.Commands.Authentication.SendEmailConfirmationTokenCommand;
using Application.Requests.Commands.Authentication.SendResetPasswordTokenCommand;
using Application.Requests.Commands.Authentication.SignUpCommand;
using Application.Requests.Queries.Authentication.FacebookSignInQuery;
using Application.Requests.Queries.Authentication.GoogleSignInQuery;
using Application.Requests.Queries.Authentication.RefreshToken;
using Application.Requests.Queries.Authentication.SignInQuery;
using Application.Responses.Authentication;
using Domain.Results;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]
[ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("sign-up")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignUp(SignUpCommand command, CancellationToken cancellationToken)
    {
        var signUpResult = await _mediator.Send(command, cancellationToken);

        if (signUpResult.IsFailure)
            return (signUpResult).ToActionResult();

        return (await _mediator.Send(new SendEmailConfirmationTokenCommand(command.Email), cancellationToken)).ToActionResult();
    }

    [HttpPost("sign-up/send-token")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> SendEmailVerificationToken(
        SendEmailConfirmationTokenCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }


    [HttpPost("sign-up/confirm-email")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmEmail(
        ConfirmEmailAndSetDefaultRoleCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpPost("sign-in")]
    [ProducesResponseType(typeof(CommonResult<SignInResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignIn([FromBody] SignInQuery signInQuery, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(signInQuery, cancellationToken)).ToActionResult();
    }

    [HttpPost("sign-in-google")]
    [ProducesResponseType(typeof(CommonResult<SignInResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignInGoogle(GoogleSignInQuery query, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(query, cancellationToken)).ToActionResult();
    }

    [HttpPost("sign-in-facebook")]
    [ProducesResponseType(typeof(CommonResult<SignInResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignInFacebook(FacebookSignInQuery query, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(query, cancellationToken)).ToActionResult();
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(CommonResult<SignInResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken(
        [FromBody] RefreshTokenQuery refreshTokenQuery,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(refreshTokenQuery, cancellationToken)).ToActionResult();
    }

    [HttpPost("password")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> SendResetPasswordConfirmationToken(
        SendResetPasswordTokenCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpPut("password/token")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmResetPassword(
        ConfirmResetPasswordCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken))
            .ToActionResult();
    }
}