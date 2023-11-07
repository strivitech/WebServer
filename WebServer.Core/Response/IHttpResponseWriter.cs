namespace WebServer.Core.Response;

public interface IHttpResponseWriter
{
    Task WriteAsync(Stream stream, HttpResponse httpResponse);
}