namespace WebServer.Core.Request.Headers;

internal interface IContentLengthValidator
{
    void ValidateContentLength(string contentLength);
}