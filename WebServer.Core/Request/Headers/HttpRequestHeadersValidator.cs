namespace WebServer.Core.Request.Headers;

internal class HttpRequestHeadersValidator : IHttpRequestHeadersValidator
{
    private readonly IContentTypeValidator _contentTypeValidator;
    private readonly IContentLengthValidator _contentLengthValidator;

    public HttpRequestHeadersValidator(IContentTypeValidator contentTypeValidator,
        IContentLengthValidator contentLengthValidator)
    {
        _contentTypeValidator = contentTypeValidator;
        _contentLengthValidator = contentLengthValidator;
    }

    public void ValidateHeaders(Dictionary<string, string> headers)
    {
        if (headers.TryGetValue("Content-Type", out var contentType))
        {
            _contentTypeValidator.ValidateContentType(contentType);
            _contentLengthValidator.ValidateContentLength(headers["Content-Length"]);
        }
        else
        {
            if (headers.ContainsKey("Content-Length"))
            {
                throw new InvalidOperationException("Content-Type header is missing");
            }
        }
    }
}