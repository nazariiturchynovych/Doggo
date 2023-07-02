namespace Doggo.Infrastructure.Services.EmailService;

using Application.Abstractions;
using Domain.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

public class EmailService : IEmailService
{
    private readonly IOptions<SMTPOptions> _options;

    public EmailService(IOptions<SMTPOptions> options)
    {
        _options = options;
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(nameof(Doggo), _options.Value.UserName));
        mimeMessage.To.Add(new MailboxAddress(to, to));
        mimeMessage.Subject = subject;

        mimeMessage.Body = new TextPart(TextFormat.Html)
        {
            Text = body
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(_options.Value.Host, 587, false);
        await client.AuthenticateAsync(_options.Value.UserName, _options.Value.Password);
        await client.SendAsync(mimeMessage);
        await client.DisconnectAsync(true);
    }
}