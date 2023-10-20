namespace WebServer.Core;

public class ControllerActionInvoker
{
    private readonly IControllerFactory _controllerFactory;

    public ControllerActionInvoker(IControllerFactory controllerFactory)
    {
        _controllerFactory = controllerFactory;
    }
    
    public async Task<IActionResult> InvokeAsync(string path)
    {
        var routePathDivider = new RoutePathDivider(path);
        var (type, instance) = _controllerFactory.Create(routePathDivider.ControllerName);
        var action = type.GetMethod(routePathDivider.ActionName);
        return await (Task<IActionResult>) action!.Invoke(instance, null)!;
    }
}