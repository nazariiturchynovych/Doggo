namespace Doggo.Application.Requests.Commands.Authentication.SendEmailConfirmationTokenCommand;

using System.Web;
using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class SendEmailConfirmationTokenCommandHandler : IRequestHandler<SendEmailConfirmationTokenCommand, CommonResult>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public SendEmailConfirmationTokenCommandHandler(UserManager<User> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<CommonResult> Handle(SendEmailConfirmationTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        string encodedString = Uri.EscapeDataString(token);

        var link = string.Format(RoutesConstants.ConfirmEmail, user.Id, encodedString);

        var body = MyClassSome.FormatHtmlBody(link);

        await _emailService.SendAsync(user.Email!, EmailMessageConstants.Subject.ValidateEmail, body);

        return Success();
    }
}