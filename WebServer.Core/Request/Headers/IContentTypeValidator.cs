namespace WebServer.Core.Request.Headers;

internal interface IContentTypeValidator
{
    void ValidateContentType(string contentType);
}