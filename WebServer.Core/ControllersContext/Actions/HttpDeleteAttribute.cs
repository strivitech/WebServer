using WebServer.Core.Common;

namespace WebServer.Core.ControllersContext.Actions;

[AttributeUsage(AttributeTargets.Method)]
public class HttpDeleteAttribute : HttpVerbAttribute
{
    public HttpDeleteAttribute() : base(HttpMethodType.Delete)
    {
    }
}