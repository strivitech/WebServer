namespace WebServer.Core;

public interface IApiHandler
{
    Task HandleAsync(Stream stream);
}