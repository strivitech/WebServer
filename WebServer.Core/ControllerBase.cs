using System.Net;

namespace WebServer.Core;

public abstract class ControllerBase : IController
{
    public static Task<IActionResult> NotFound(object? content = null)
    {
        return Task.FromResult<IActionResult>(new ObjectResult(content, HttpStatusCode.NotFound));
    }
    
    public static Task<IActionResult> Ok(object? content = null)
    {
        return Task.FromResult<IActionResult>(new ObjectResult(content, HttpStatusCode.OK));
    }
}