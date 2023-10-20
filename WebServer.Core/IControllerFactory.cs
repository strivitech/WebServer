namespace WebServer.Core;

public interface IControllerFactory
{
    (Type Type, object Instance) Create(string controllerName);   
}