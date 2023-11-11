namespace WebServer.Core.Request;

internal interface IHttpRequestHandler
{
    Task HandleAsync(Stream stream);
}