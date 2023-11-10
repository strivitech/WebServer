using WebServer.Core.Common;

namespace WebServer.Core.ControllersContext.Actions;

[AttributeUsage(AttributeTargets.Method)]
public class HttpGetAttribute : HttpVerbAttribute
{
    public HttpGetAttribute() : base(HttpMethodType.Get)
    {
    }
}