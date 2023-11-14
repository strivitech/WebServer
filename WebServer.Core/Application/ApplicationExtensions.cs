using WebServer.Core.MinimalApiContext;

namespace WebServer.Core.Application;

public static class ApplicationExtensions
{
    public static IEndpointsBuilder UseEndpoints(this App app)
    {
        return new EndpointsBuilder();
    }
}