namespace WebServer.Core.ControllersContext.Actions;

public class ActionInfoFetcherFactory : IActionInfoFetcherFactory
{
    public IActionInternalInfoFetcher Create(ControllerInternalInfo controllerInternalInfo, string httpMethod,
        string? actionName)
    {
        return controllerInternalInfo.IsRest
            ? new RestActionInfoFetcher(httpMethod, controllerInternalInfo.Type.Name)
            : new OrdinalActionInfoFetcher(httpMethod, controllerInternalInfo.Type.Name,
                actionName ?? throw new InvalidOperationException("Action name is null"));
    }
}