namespace WebServer.Responses;

public class DeletePersonResponse
{
    public string Id { get; set; } = null!;

    public bool IsDeleted { get; set; }
}