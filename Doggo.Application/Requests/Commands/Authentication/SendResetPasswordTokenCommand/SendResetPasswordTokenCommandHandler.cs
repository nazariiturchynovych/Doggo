namespace Doggo.Application.Requests.Commands.Authentication.SendResetPasswordTokenCommand;

using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class SendResetPasswordTokenCommandHandler : IRequestHandler<SendResetPasswordTokenCommand, CommonResult>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public SendResetPasswordTokenCommandHandler(
        UserManager<User> userManager,
        IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<CommonResult> Handle(SendResetPasswordTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.UserEmail);

        if (user is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var link = string.Format(RoutesConstants.ConfirmResetPasswordCommand, user.Id, token);

        var body = string.Format(EmailMessageConstants.MyClass.ResetPassword, link);

        await _emailService.SendAsync(user.Email!, EmailMessageConstants.Subject.ResetPassword, body);

        return Success();
    }
}