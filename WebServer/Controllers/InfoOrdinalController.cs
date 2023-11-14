using WebServer.Core.ControllersContext;
using WebServer.Core.ControllersContext.Actions;
using WebServer.Requests;

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

    [HttpGet]
    public Task<IActionResult> GetMyData(
        [FromBody] GetPersonRequest request,
        [FromParameters] string country,
        [FromParameters] string city,
        [FromQuery] int age
        )
    {   
        return Task.FromResult(Ok(new
        {
            request.Id,
            Country = country,
            City = city,
            Age = age
        }));
    }
}