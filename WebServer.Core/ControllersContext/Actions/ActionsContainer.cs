using System.Reflection;

namespace WebServer.Core.ControllersContext.Actions;

public static class ActionsContainer
{
    public static readonly Dictionary<string, PropertyInfo[]> FullNameToProperties =
        ControllersContainer.ControllerNameToMethodsInfo.Values
            .SelectMany(methods => methods
                .SelectMany(ai => ai.Parameters
                    .Where(pi => pi.CustomAttributes.Count() == 1 &&
                                 pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(FromQueryAttribute)))
                    .Select(pi => new
                    {
                        FullName = pi.ParameterType.FullName,
                        Properties = pi.ParameterType.GetProperties()
                    })))
            .GroupBy(x => x.FullName!)
            .ToDictionary(g =>
                    g.Key,
                g => g.SelectMany(x => x.Properties).Distinct().ToArray());
}