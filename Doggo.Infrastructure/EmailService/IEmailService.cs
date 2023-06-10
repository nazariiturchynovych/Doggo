namespace Doggo.Infrastructure.EmailService;

using System.Net.Mail;

public interface IEmailService
{
    public Task SendAsync(MailMessage message);
}