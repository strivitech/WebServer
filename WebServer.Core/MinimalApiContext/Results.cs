using System.Net;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.MinimalApiContext;

public static class Results
{
    public static IActionResult NotFound(object? content = null)
    {
        return new ObjectResult(content, HttpStatusCode.NotFound);
    }
    
    public static IActionResult Ok(object? content = null)
    {
        return new ObjectResult(content, HttpStatusCode.OK);
    }
    
    public static IActionResult BadRequest(object? content = null)
    {
        return new ObjectResult(content, HttpStatusCode.BadRequest);
    }
}