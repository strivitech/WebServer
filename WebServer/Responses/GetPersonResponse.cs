using WebServer.Dtos;

namespace WebServer.Responses;

public class GetPersonResponse
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public List<string> Hobbies { get; set; } = null!;

    public LocationDto CurrentLocation { get; set; } = null!;

    public List<LocationDto> Locations { get; set; } = null!;
}