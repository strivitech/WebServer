using System.Reflection;

namespace WebServer.Core;

public class ControllerActionInvoker
{
    private readonly IControllerFactory _controllerFactory;

    public ControllerActionInvoker(IControllerFactory controllerFactory)
    {
        _controllerFactory = controllerFactory;
    }

    public async Task<IActionResult> InvokeAsync(string path, params object?[]? args)
    {
        var webRequestRoute = new WebRequestPathParser(path).Parse();
        var controllerInternalInfo = GetControllerInternalInfo(path, webRequestRoute);
        var methodInternalInfo = GetMethodInternalInfo(args, controllerInternalInfo, webRequestRoute);

        var instance = _controllerFactory.Create(controllerInternalInfo.Type);
        return await (Task<IActionResult>)methodInternalInfo.MethodInfo.Invoke(instance, args)!;
    }

    private static ActionInternalInfo GetMethodInternalInfo(object?[]? args, ControllerInternalInfo controllerInternalInfo,
        WebRequestRoute webRequestRoute)
    {
        if (!ControllersContainer.ControllerNameToMethodsInfo.TryGetValue(controllerInternalInfo.Type.Name,
                out var methodsInternalInfo))
        {
            throw new IndexOutOfRangeException(
                $"Action {webRequestRoute.ActionName} not found in controller {controllerInternalInfo.Type.Name}");
        }
        
        var methodInternalInfo = methodsInternalInfo.SingleOrDefault(m =>
            string.Equals(m.Name, webRequestRoute.ActionName, StringComparison.OrdinalIgnoreCase)
            && AllParametersMatchingArgumentsTypes(m.MethodInfo, args));

        if (methodInternalInfo is null)
        {
            throw new InvalidOperationException(
                $"Action {webRequestRoute.ActionName} not found in controller {controllerInternalInfo.Type.Name}");
        }

        return methodInternalInfo;
    }

    private static ControllerInternalInfo GetControllerInternalInfo(string path, WebRequestRoute webRequestRoute)
    {
        if (!ControllersContainer.PathToControllerInfo.TryGetValue(webRequestRoute.ControllerRoute,
                out var controllerInternalInfo))
        {
            throw new IndexOutOfRangeException($"Controller for path '{path}' not found");
        }

        return controllerInternalInfo;
    }

    private static bool AllParametersMatchingArgumentsTypes(MethodInfo methodInfo, object?[]? args)
    {
        var parameterTypes = methodInfo.GetParameters().Select(pi => pi.ParameterType).ToArray();
        var argumentTypes = (args ?? Array.Empty<object?>()).Select(a => a?.GetType()).ToArray();

        return parameterTypes.Length == argumentTypes.Length &&
               !parameterTypes.Where((t, i) => t != argumentTypes[i]).Any();
    }
}