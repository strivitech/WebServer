using WebServer.Core.MinimalApiContext;

namespace WebServer.Core.Application;

public class AppBuilder : IAppBuilder
{
    public IEndpointsBuilder UseEndpoints()
    {
        return new EndpointsBuilder();
    }
}