using WebServer.Core.Common;

namespace WebServer.Core.ControllersContext.Actions;

[AttributeUsage(AttributeTargets.Method)]
public class HttpHeadAttribute : HttpVerbAttribute
{
    public HttpHeadAttribute() : base(HttpMethodType.Head)
    {
    }
}