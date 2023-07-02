namespace Doggo.Application.Abstractions;

public interface IEmailService
{
    public Task SendAsync(string to, string subject, string body);
}