namespace WebServer.Core;

public class ControllerFactory : IControllerFactory
{
    public object Create(Type controllerType)
    {
        return Activator.CreateInstance(controllerType)!;
    }
}