namespace WebServer.Core;

public class RoutePathDivider
{
    public RoutePathDivider(string path)
    {
        SplitBySlash(path);
    }
    
    public string ControllerName { get; private set; } = null!;
    public string ActionName { get; private set; } = null!;

    private void SplitBySlash(string path)
    {
        var parts = path.Split('/');
        ControllerName = parts[1];
        ActionName = parts[2];
    }
}