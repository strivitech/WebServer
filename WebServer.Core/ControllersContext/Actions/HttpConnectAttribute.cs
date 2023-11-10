using WebServer.Core.Common;

namespace WebServer.Core.ControllersContext.Actions;

[AttributeUsage(AttributeTargets.Method)]
public class HttpConnectAttribute : HttpVerbAttribute
{
    public HttpConnectAttribute() : base(HttpMethodType.Connect)
    {
    }
}