using WebServer.Core.ControllersContext;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Controllers;

[Route("/api/Information")]
public class InfoOrdinalController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> GetInfo()
    {
        return Task.FromResult(Ok(new
        {
            ServerInformation = new
            {
                ServerName = "WebServer",
                ServerVersion = "1.0.0",
                ServerPort = 443
            },
        }));
    }
}