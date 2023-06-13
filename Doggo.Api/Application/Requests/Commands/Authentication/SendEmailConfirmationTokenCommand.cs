namespace Doggo.Application.Requests.Commands.Authentication;

using System.Net;
using System.Net.Mail;
using Domain.Constants.ErrorConstants;
using Domain.Entities.User;
using Domain.Options;
using Domain.Results.Abstract;
using Infrastructure.Services.EmailService;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

public record SendEmailConfirmationTokenCommand(string UserEmail, string Link) : IRequest<ICommonResult>
{
    public class Handler : IRequestHandler<SendEmailConfirmationTokenCommand, ICommonResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly SMTPOptions _options;

        public Handler(UserManager<User> userManager, IOptions<SMTPOptions> options, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _options = options.Value;
        }

        public async Task<ICommonResult> Handle(SendEmailConfirmationTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.UserEmail);

            if (user is null)
                return Failure(UserErrors.UserDoesNotExist);

            var userId = user.Id;

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var link =
                $"{request.Link}" + $"?{nameof(userId)}={userId}" + $"&{nameof(token)}={WebUtility.UrlEncode(token)}";

            var message = new MailMessage(_options.UserName, request.UserEmail)
            {
                IsBodyHtml = true,
                Body = $"Activate account: <a href = '{link}'> link </a>",
            };

            await _emailService.SendAsync(message);

            return Success();
        }
    }
}