namespace Doggo.Application.Abstractions.Services;

public interface IEmailService
{
    public Task SendAsync(string to, string subject, string body);
}