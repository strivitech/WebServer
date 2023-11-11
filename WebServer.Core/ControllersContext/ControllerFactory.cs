namespace WebServer.Core.ControllersContext;

internal class ControllerFactory : IControllerFactory
{
    public object Create(Type controllerType)
    {
        return Activator.CreateInstance(controllerType)!;
    }
}