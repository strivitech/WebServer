using System.Reflection;
using WebServer.Core.ControllersContext.Actions;

namespace WebServer.Core.ControllersContext;

public class ControllerInternalInfo
{
    public ControllerInternalInfo(Type type)
    {
        Type = type;
        Route = type.GetCustomAttribute<RouteAttribute>()!.Template;
        CustomAttributeData = type.GetCustomAttributesData();
        Methods = GetMethods();
    }

    public Type Type { get; }

    public string Route { get; }

    public IList<CustomAttributeData> CustomAttributeData { get; }

    public IList<ActionInternalInfo> Methods { get; }

    private IList<ActionInternalInfo> GetMethods()
    {
        var methodGroups = Type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(m => m.GetCustomAttribute<ActionNameAttribute>() != null)
            .GroupBy(m => m.Name)
            .ToList();

        var duplicateMethodNames = methodGroups
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToList();

        if (duplicateMethodNames.Count != 0)
        {
            throw new InvalidOperationException(
                $"Ambiguous method names detected: {string.Join(", ", duplicateMethodNames)}");
        }

        return methodGroups.SelectMany(group =>
            group.Select(m => new ActionInternalInfo(m))).ToList();
    }
}