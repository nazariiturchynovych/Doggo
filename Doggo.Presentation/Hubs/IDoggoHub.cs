namespace Doggo.Presentation.Hubs;

public interface IDoggoHub
{
    public Task OnError(string error);
    public Task ReceiveMessage(string message);
}