namespace WebServer.Core.Request;

public interface IHttpRequestReader
{   
    Task<HttpRequest> ReadAsync(Stream stream);
}