using System.Net;

namespace WebServer.Core;

public interface IActionResult
{
    public HttpStatusCode StatusCode { get; }
    
    public object? Content { get; }
}