namespace WebServer.Core.Request.Headers;

internal class ContentLengthValidator : IContentLengthValidator
{
    public void ValidateContentLength(string contentLength)
    {
        if (!int.TryParse(contentLength, out var parsedContentLength) || parsedContentLength < 0)
        {
            throw new InvalidOperationException("Content-Length header is invalid");
        }
    }
}