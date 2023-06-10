namespace Doggo.Application.Requests.Commands.User;

using System.Net;
using System.Net.Mail;
using ResultFactory;
using Doggo.Domain.Entities.User;
using Doggo.Domain.Results.Errors;
using Domain.Options;
using Domain.Results.Abstract;
using Infrastructure.EmailService;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

public record SendResetPasswordTokenCommand(string UserEmail, string Link) : IRequest<ICommonResult>
{
    public class Handler : IRequestHandler<SendResetPasswordTokenCommand, ICommonResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly SMTPOptions _options;

        public Handler(UserManager<User> userManager, IEmailService emailService, IOptions<SMTPOptions> options)
        {
            _userManager = userManager;
            _emailService = emailService;
            _options = options.Value;
        }

        public async Task<ICommonResult> Handle(SendResetPasswordTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.UserEmail);

            if (user is null)
                return ResultFactory.Failure(UserErrors.UserDoesNotExist);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var userId = user.Id;

            var link =
                $"{request.Link}" + $"?{nameof(userId)}={userId}" + $"&{nameof(token)}={WebUtility.UrlEncode(token)}";

            var message = new MailMessage(_options.UserName, request.UserEmail)
            {
                IsBodyHtml = true,
                Body = $"Reset password: <a href = '{link}'> link </a>",
            };

            await _emailService.SendAsync(message);

            return ResultFactory.Success();
        }
    }
}