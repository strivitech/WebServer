using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.Request;

internal interface IHttpRequestProcessor
{
    Task<IActionResult> CompleteAsync(HttpRequest httpRequest);
}