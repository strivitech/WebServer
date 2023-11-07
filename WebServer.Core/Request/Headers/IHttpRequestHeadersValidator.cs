namespace WebServer.Core.Request.Headers;

public interface IHttpRequestHeadersValidator
{
    void ValidateHeaders(Dictionary<string, string> headers);
}