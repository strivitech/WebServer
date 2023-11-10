using System.Net;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.MinimalApiContext;

public static class Results
{
    public static Task<IActionResult> Ok(object? content = null)
    {
        return Task.FromResult<IActionResult>(new ObjectResult(content, HttpStatusCode.OK));
    }
}