using WebServer.Core.Common;

namespace WebServer.Core.ControllersContext.Actions;

[AttributeUsage(AttributeTargets.Method)]
public class HttpTraceAttribute : HttpVerbAttribute
{
    public HttpTraceAttribute() : base(HttpMethodType.Trace)
    {
    }
}