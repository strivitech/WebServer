namespace WebServer.Core;

public interface IControllerFactory
{
    object Create(Type controllerType);
}