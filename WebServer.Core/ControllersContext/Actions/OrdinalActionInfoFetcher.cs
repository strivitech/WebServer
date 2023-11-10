namespace WebServer.Core.ControllersContext.Actions;

public class OrdinalActionInfoFetcher : IActionInternalInfoFetcher
{
    private readonly string _httpMethod;
    private readonly string _controllerName;
    private readonly string _actionName;

    public OrdinalActionInfoFetcher(string httpMethod, string controllerName,
        string actionName)
    {
        _httpMethod = httpMethod;
        _controllerName = controllerName;
        _actionName = actionName;
    }

    public ActionInternalInfo Get()
    {
        if (!ControllersContainer.ControllerNameToMethodsInfo.TryGetValue(_controllerName,
                out var methodsInternalInfo))
        {
            throw new IndexOutOfRangeException(
                $"Action {_actionName} not found in controller {_controllerName}");
        }

        var methodInternalInfo = methodsInternalInfo.SingleOrDefault(m =>
            string.Equals(m.Name, _actionName, StringComparison.OrdinalIgnoreCase)
            && string.Equals(m.HttpMethodTypeValue.ToString(), _httpMethod, StringComparison.OrdinalIgnoreCase));

        if (methodInternalInfo is null)
        {
            throw new InvalidOperationException(
                $"Action {_actionName} not found in controller {_controllerName}");
        }

        return methodInternalInfo;
    }
}