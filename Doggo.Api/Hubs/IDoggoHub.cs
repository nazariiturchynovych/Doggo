namespace Doggo.Hubs;

public interface IDoggoHub
{
    Task ClientHook(string message);
}