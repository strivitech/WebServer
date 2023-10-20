using System.Net;

namespace WebServer.Core;

public class ObjectResult : IActionResult
{
    public ObjectResult(object? content, HttpStatusCode statusCode)
    {
        Content = content;
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; }

    public object? Content { get; }  
}