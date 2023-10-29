namespace WebServer.Core;

public class WebRequestRoute
{
    public string ControllerRoute { get; }

    public string ActionName { get; }

    public List<string> UrlParameters { get; }

    public Dictionary<string, string?> QueryParameters { get; }

    public WebRequestRoute(string controllerRoute, string actionName, List<string> urlParameters,
        Dictionary<string, string?> queryParameters)
    {
        ControllerRoute = controllerRoute;
        ActionName = actionName;
        UrlParameters = urlParameters;
        QueryParameters = queryParameters;
    }
}