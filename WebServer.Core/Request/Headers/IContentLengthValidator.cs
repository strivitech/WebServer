namespace WebServer.Core.Request.Headers;

public interface IContentLengthValidator
{
    void ValidateContentLength(string contentLength);
}