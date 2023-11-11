namespace WebServer.Core.Request;

internal interface IHttpRequestReader
{   
    Task<HttpRequest> ReadAsync(Stream stream);
}