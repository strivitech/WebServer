namespace WebServer.Core.ControllersContext;

internal interface IControllerFactory
{
    object Create(Type controllerType);
}