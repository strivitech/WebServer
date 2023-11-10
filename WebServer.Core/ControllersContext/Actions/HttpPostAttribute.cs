using WebServer.Core.Common;

namespace WebServer.Core.ControllersContext.Actions;

[AttributeUsage(AttributeTargets.Method)]
public class HttpPostAttribute : HttpVerbAttribute
{
    public HttpPostAttribute() : base(HttpMethodType.Post)
    {
    }
}