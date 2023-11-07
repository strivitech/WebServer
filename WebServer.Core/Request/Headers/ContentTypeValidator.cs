namespace WebServer.Core.Request.Headers;

public class ContentTypeValidator : IContentTypeValidator
{
    public void ValidateContentType(string contentType)
    {
        if (!contentType.Contains("application/json"))
        {
            throw new InvalidOperationException("Content-Type header is not application/json");
        }
    }
}