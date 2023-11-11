namespace WebServer.Core.Response;

internal interface IHttpResponseWriter
{
    Task WriteAsync(Stream stream, HttpResponse httpResponse);
}