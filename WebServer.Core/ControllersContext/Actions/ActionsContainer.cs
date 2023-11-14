using System.Collections.Frozen;
using System.Reflection;

namespace WebServer.Core.ControllersContext.Actions;

internal static class ActionsContainer
{
    public static readonly FrozenDictionary<string, PropertyInfo[]> FullNameToProperties =
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
            .ToFrozenDictionary(g =>
                    g.Key,
                g => g.SelectMany(x => x.Properties).Distinct().ToArray());
}