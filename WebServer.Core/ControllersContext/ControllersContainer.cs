using System.Reflection;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.ControllersContext;

public static class ControllersContainer
{
    public static readonly Dictionary<string, ControllerInternalInfo> PathToControllerInfo =
        AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.IsAssignableTo(typeof(IController)) && t is { IsAbstract: false, IsInterface: false })
            .Select(t => new { Type = t, Route = t.GetCustomAttribute<RouteAttribute>()!.Template })
            .ToDictionary(typeRoute => typeRoute.Route, typeRoute => new ControllerInternalInfo(typeRoute.Type));

    public static readonly Dictionary<string, List<ActionInternalInfo>> ControllerNameToMethodsInfo =
        PathToControllerInfo.Values
            .SelectMany(
                controller => controller.Methods,
                (controller, method) => (ControllerName: controller.Type.Name, MethodInfo: method))
            .GroupBy(tuple => tuple.ControllerName, tuple => tuple.MethodInfo)
            .ToDictionary(group => group.Key, group => group.ToList());
}