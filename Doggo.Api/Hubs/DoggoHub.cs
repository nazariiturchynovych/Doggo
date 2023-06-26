namespace Doggo.Hubs;

using Microsoft.AspNetCore.SignalR;

public sealed class NotificationsHub : Hub
{
    public async Task SendNotification(string content)
    {
        var a = "from hub";
        Console.WriteLine(content);
        await Clients.All.SendAsync("ReceiveNotification", a);
    }
}