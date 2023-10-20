using System.Text.Json;

namespace WebServer.Core;

public class ControllerFactory : IControllerFactory
{
    public (Type Type, object Instance) Create(string controllerName)
    {
        Console.WriteLine(JsonSerializer.Serialize(ControllerTypes.NameTypeDictionary.Select(kp => new
        {
            kp.Key,
            kp.Value.FullName
        })));
        var controllerType = ControllerTypes.NameTypeDictionary[controllerName];
        return (controllerType, Activator.CreateInstance(controllerType)!);
    }
}