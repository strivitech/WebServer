using System.Reflection;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.ControllersContext;

public class ControllerInternalInfo
{
    public ControllerInternalInfo(Type type)
    {
        Type = type;
        Route = type.GetCustomAttribute<RouteAttribute>()!.Template;
        IsRest = type.GetCustomAttribute<RestAttribute>() is not null;
        CustomAttributeData = type.GetCustomAttributesData();
        Actions = GetActions();
    }

    public Type Type { get; }

    public string Route { get; }

    public bool IsRest { get; }

    public IList<CustomAttributeData> CustomAttributeData { get; }

    public IList<ActionInternalInfo> Actions { get; }

    private List<ActionInternalInfo> GetActions()
    {
        var methodGroups = Type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(m => m.GetCustomAttribute<HttpVerbAttribute>() is not null)
            .GroupBy(m => m.Name)
            .ToList();

        RequireNoNameDuplicates(methodGroups);
        
        if (IsRest)
        {
            RequireNoHttpMethodDuplicates(methodGroups);
        }

        return methodGroups.SelectMany(group =>
            group.Select(m => new ActionInternalInfo(m))).ToList();
    }

    private void RequireNoHttpMethodDuplicates(List<IGrouping<string, MethodInfo>> methodGroups)
    {
        var duplicateHttpMethods = methodGroups
            .SelectMany(group => group.Select(m => m.GetCustomAttribute<HttpVerbAttribute>()!.Method))
            .GroupBy(method => method)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToList();

        if (duplicateHttpMethods.Count != 0)
        {
            throw new InvalidOperationException(
                $"Ambiguous HTTP methods detected: {string.Join(", ", duplicateHttpMethods)}");
        }
    }

    private static void RequireNoNameDuplicates(List<IGrouping<string, MethodInfo>> methodGroups)
    {
        var duplicateMethodNames = methodGroups
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToList();

        if (duplicateMethodNames.Count != 0)
        {
            throw new InvalidOperationException(
                $"Ambiguous method names detected: {string.Join(", ", duplicateMethodNames)}");
        }
    }
}