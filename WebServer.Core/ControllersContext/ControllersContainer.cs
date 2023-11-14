using System.Collections.Frozen;
using System.Reflection;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.ControllersContext;

internal static class ControllersContainer
{
    public static readonly FrozenDictionary<string, ControllerInternalInfo> PathToControllerInfo =
        AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.IsAssignableTo(typeof(IController)) && t is { IsAbstract: false, IsInterface: false })
            .Select(t => new { Type = t, Route = t.GetCustomAttribute<RouteAttribute>()!.Template })
            .ToFrozenDictionary(typeRoute => typeRoute.Route, typeRoute => new ControllerInternalInfo(typeRoute.Type));

    public static readonly FrozenDictionary<string, List<ActionInternalInfo>> ControllerNameToMethodsInfo =
        PathToControllerInfo.Values
            .SelectMany(
                controller => controller.Actions,
                (controller, method) => (ControllerName: controller.Type.Name, MethodInfo: method))
            .GroupBy(tuple => tuple.ControllerName, tuple => tuple.MethodInfo)
            .ToFrozenDictionary(group => group.Key, group => group.ToList());
}