namespace WebServer.Core.ControllersContext;

public interface IControllerFactory
{
    object Create(Type controllerType);
}