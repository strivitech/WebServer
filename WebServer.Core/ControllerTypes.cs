using System.Reflection;

namespace WebServer.Core;

public static class ControllerTypes
{
    public static readonly Dictionary<string, Type> NameTypeDictionary = Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.IsAssignableTo(typeof(IController)) && t is { IsAbstract: false, IsInterface: false })
        .ToDictionary(t => t.Name, t => t);
}