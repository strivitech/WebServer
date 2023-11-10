using WebServer.Core.Common;

namespace WebServer.Core.ControllersContext.Actions;

[AttributeUsage(AttributeTargets.Method)]
public class HttpPutAttribute : HttpVerbAttribute
{
    public HttpPutAttribute() : base(HttpMethodType.Put)
    {
    }
}