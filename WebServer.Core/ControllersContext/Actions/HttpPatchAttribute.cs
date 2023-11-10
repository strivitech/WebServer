using WebServer.Core.Common;

namespace WebServer.Core.ControllersContext.Actions;

[AttributeUsage(AttributeTargets.Method)]
public class HttpPatchAttribute : HttpVerbAttribute
{
    public HttpPatchAttribute() : base(HttpMethodType.Patch)
    {
    }
}