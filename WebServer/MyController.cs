using WebServer.Core.ControllersContext;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer;

[Route($"/api/{nameof(MyController)}")]
public class MyController : ControllerBase
{
    [HttpPost]
    [ActionName(nameof(PostPersonInfo))]
    public async Task<IActionResult> PostPersonInfo(
        [FromBody] Person person,
        [FromParameters] string country,
        [FromParameters] string city,
        [FromParameters] string street,
        [FromQuery] Car car)
    {   
        return await Ok(new
        {
            Person = person,
            Address = new
            {
                Country = country,
                City = city,
                Street = street
            },
            Car = car
        });
    }
}

public class Person
{
    public string Name { get; set; } = null!;

    public int Age { get; set; }
}

public class Car
{
    public string Model { get; set; } = null!;
    
    public int Year { get; set; }
}