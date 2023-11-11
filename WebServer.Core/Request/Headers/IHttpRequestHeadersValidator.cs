namespace WebServer.Core.Request.Headers;

internal interface IHttpRequestHeadersValidator
{
    void ValidateHeaders(Dictionary<string, string> headers);
}