namespace WebServer.Core.ControllersContext.Actions;

public static class ActionInternalInfoFetcher
{
    public static ActionInternalInfo Get(string httpMethod, string controllerName,
        string actionName)
    {
        if (!ControllersContainer.ControllerNameToMethodsInfo.TryGetValue(controllerName,
                out var methodsInternalInfo))
        {
            throw new IndexOutOfRangeException(
                $"Action {actionName} not found in controller {controllerName}");
        }

        var methodInternalInfo = methodsInternalInfo.SingleOrDefault(m =>
            string.Equals(m.Name, actionName, StringComparison.OrdinalIgnoreCase)
            && string.Equals(m.HttpMethodValue.ToString(), httpMethod, StringComparison.OrdinalIgnoreCase));

        if (methodInternalInfo is null)
        {
            throw new InvalidOperationException(
                $"Action {actionName} not found in controller {controllerName}");
        }

        return methodInternalInfo;
    }
}