using System.Net;

namespace WebServer.Core.ControllersContext.Actions;

public interface IActionResult
{
    public HttpStatusCode StatusCode { get; }
    
    public object? Content { get; }
}