using WebServer.Dtos;

namespace WebServer.Requests;

public class UpdatePersonRequest
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Age { get; set; }
    
    public List<string> Hobbies { get; set; } = null!;
    
    public LocationDto CurrentLocation { get; set; } = null!;
    
    public List<LocationDto> Locations { get; set; } = null!;
}