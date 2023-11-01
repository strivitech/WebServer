namespace WebServer.Core.ControllersContext;

[AttributeUsage(AttributeTargets.Class)]
public class RouteAttribute : Attribute
{
    public RouteAttribute(string template)
    {
        Template = template;
    }

    public string Template { get; set; }
}