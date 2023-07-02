namespace Doggo.Application.Requests.Commands.Authentication;

using System.Net;
using System.Net.Mail;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Options;
using Domain.Results;
using Infrastructure.Services.EmailService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

public record SendResetPasswordTokenCommand(string UserEmail, string NewPassword, string ConfirmPassword)
    : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<SendResetPasswordTokenCommand, CommonResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IUrlHelper _urlHelper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly SMTPOptions _options;

        public Handler(
            UserManager<User> userManager,
            IEmailService emailService,
            IOptions<SMTPOptions> options,
            IUrlHelper urlHelper,
            IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _emailService = emailService;
            _urlHelper = urlHelper;
            _httpContext = httpContext;
            _options = options.Value;
        }

        public async Task<CommonResult> Handle(SendResetPasswordTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.UserEmail);

            if (user is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var userId = user.Id;

            // var actionUrl = _urlHelper.Action(
            //     "ConfirmResetPasswordCommand",
            //     "Authentication",
            //     values: new
            //     {
            //         token = WebUtility.UrlEncode(token),
            //         userId,
            //         newPassword = request.NewPassword
            //     },
            //     _httpContext.HttpContext!.Request.Scheme);

            // var link =
            //     $"{actionUrl}" + $"?{nameof(userId)}={userId}" + $"&{nameof(token)}={WebUtility.UrlEncode(token)}";

            var message = new MailMessage(_options.UserName, request.UserEmail)
            {
                IsBodyHtml = true,
                // Body = $"Reset password: <a href = '{actionUrl}'> link </a>",
            };

            await _emailService.SendAsync(message);

            return Success();
        }
    }
}