namespace Doggo.Infrastructure.Services.EmailService;

using System.Net.Mail;

public interface IEmailService
{
    public Task SendAsync(MailMessage message);
}