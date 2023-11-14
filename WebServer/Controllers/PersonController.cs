using WebServer.Core.ControllersContext;
using WebServer.Core.ControllersContext.Actions;
using WebServer.Requests;
using WebServer.Services;

namespace WebServer.Controllers;

[Rest]
[Route("/api/Person")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService = new PersonService();

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPersonRequest request)
    {
        try
        {
            return Ok(await _personService.GetAsync(request));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonRequest request)
    {
        try
        {
            return Ok(await _personService.CreateAsync(request));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePersonRequest request)
    {
        try
        {
            return Ok(await _personService.UpdateAsync(request));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeletePersonRequest request)
    {
        try
        {
            return Ok(await _personService.DeleteAsync(request));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}