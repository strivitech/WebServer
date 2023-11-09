namespace WebServer.Core.Request;

public interface IHttpRequestHandler
{
    Task HandleAsync(Stream stream);
}