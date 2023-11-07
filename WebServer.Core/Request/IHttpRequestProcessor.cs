using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.Request;

public interface IHttpRequestProcessor
{
    Task<IActionResult> CompleteAsync(HttpRequest httpRequest);
}