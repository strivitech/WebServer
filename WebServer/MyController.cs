﻿using WebServer.Core.ControllersContext;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer;

[Route($"/api/{nameof(MyController)}")]
public class MyController : ControllerBase
{
    [HttpPost]
    [ActionName(nameof(PostInfo))]
    public async Task<IActionResult> PostInfo(
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
    
    public List<string> Hobbies { get; set; } = null!;
}
    
public class Car
{
    public string Model { get; set; } = null!;

    public int Year { get; set; }
}