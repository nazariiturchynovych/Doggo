namespace Doggo.Hubs;

using Microsoft.AspNetCore.SignalR;

public sealed class NotificationsHub : Hub
{
    public async Task SendNotification()
    {
        var a = "from hub";
        Console.WriteLine();
        await Clients.All.SendAsync("ReceiveNotification", a);
    }
}