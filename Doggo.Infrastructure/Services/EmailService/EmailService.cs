namespace Doggo.Infrastructure.Services.EmailService;

using System.Net;
using System.Net.Mail;
using Domain.Options;
using Microsoft.Extensions.Options;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;

    public EmailService(IOptions<SMTPOptions> options)
    {
        var options1 = options.Value;
        _smtpClient = new SmtpClient()
        {
            Host = options1.Host,
            Port = options1.Port,
            Credentials = new NetworkCredential(options1.UserName, options1.Password),
            EnableSsl = true
        };
    }

    public Task SendAsync(MailMessage message)
    {
        return _smtpClient
            .SendMailAsync(message);
    }
}