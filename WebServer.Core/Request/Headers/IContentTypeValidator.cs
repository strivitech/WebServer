namespace WebServer.Core.Request.Headers;

public interface IContentTypeValidator
{
    void ValidateContentType(string contentType);
}