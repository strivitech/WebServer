namespace WebServer.Core.ControllersContext.Actions;

public interface IActionInfoFetcherFactory
{
    IActionInternalInfoFetcher Create(ControllerInternalInfo controllerInternalInfo, string httpMethod,
        string? actionName);    
}