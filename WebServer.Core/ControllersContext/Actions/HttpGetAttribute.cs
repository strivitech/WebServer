using HttpMethod = WebServer.Core.Common.HttpMethod;

namespace WebServer.Core.ControllersContext.Actions;

[AttributeUsage(AttributeTargets.Method)]
public class HttpGetAttribute : HttpVerbAttribute
{
    public HttpGetAttribute() : base(HttpMethod.Get)
    {
    }
}