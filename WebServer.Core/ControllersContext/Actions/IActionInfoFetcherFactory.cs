namespace WebServer.Core.ControllersContext.Actions;

internal interface IActionInfoFetcherFactory
{
    IActionInternalInfoFetcher Create(ControllerInternalInfo controllerInternalInfo, string httpMethod,
        string? actionName);    
}