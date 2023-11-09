namespace WebServer.Core.ControllersContext.Actions;

public class RestActionInfoFetcher : IActionInternalInfoFetcher
{
    private readonly string _httpMethod;
    private readonly string _controllerName;

    public RestActionInfoFetcher(string httpMethod, string controllerName)
    {
        _httpMethod = httpMethod;
        _controllerName = controllerName;
    }

    public ActionInternalInfo Get()
    {
        if (!ControllersContainer.ControllerNameToMethodsInfo.TryGetValue(_controllerName,
                out var methodsInternalInfo))
        {
            throw new IndexOutOfRangeException(
                $"Action with such {_httpMethod} not found in controller {_controllerName}");
        }

        var methodInternalInfo = methodsInternalInfo.SingleOrDefault(m =>
            string.Equals(m.HttpMethodValue.ToString(), _httpMethod, StringComparison.OrdinalIgnoreCase));

        if (methodInternalInfo is null)
        {
            throw new InvalidOperationException(
                $"Action with such {_httpMethod} not found in controller {_controllerName}");
        }

        return methodInternalInfo;
    }
}