namespace WebServer.Core.Request;

internal class WebRequestRoute
{
    public string OperatingPath { get; }

    public string? ActionName { get; }

    public List<string> UrlParameters { get; }

    public Dictionary<string, string?> QueryParameters { get; }

    public WebRequestRoute(string operatingPath, string? actionName, List<string> urlParameters,
        Dictionary<string, string?> queryParameters)
    {
        OperatingPath = operatingPath;
        ActionName = actionName;
        UrlParameters = urlParameters;
        QueryParameters = queryParameters;
    }
}