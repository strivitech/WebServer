using WebServer.Core.MinimalApiContext;

namespace WebServer.Core.Application;

public interface IAppBuilder : IEndpointsBuilder
{
    IEndpointsBuilder UseEndpoints();
}