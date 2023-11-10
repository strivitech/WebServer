using WebServer.Core.Common;

namespace WebServer.Core.ControllersContext.Actions;

[AttributeUsage(AttributeTargets.Method)]
public class HttpOptionsAttribute : HttpVerbAttribute
{
    public HttpOptionsAttribute() : base(HttpMethodType.Options)
    {
    }
}